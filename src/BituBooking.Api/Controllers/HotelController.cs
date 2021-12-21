#nullable disable
namespace BituBooking.Api.Controllers;

using BituBooking.Api.ApiModels;
using BituBooking.Domain.Management.Commands;
using BituBooking.Reading.Queries;
using BituBooking.SharedKernell.Context;
using BituBooking.SharedKernell.Queries;

using MediatR;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("hotel")]
public class HotelController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ITransactionManager _transactionManager;
    private readonly ILogger<HotelController> _logger;

    public HotelController(IMediator mediator, ITransactionManager transactionManager, ILogger<HotelController> logger)
    {
        _mediator = mediator;
        _transactionManager = transactionManager;
        _logger = logger;
    }

    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<ActionResult> GetByIdAsync(Guid id, CancellationToken ct)
    {
        _logger.LogInformation("GetbyIdAsync");
        var data = await _mediator.DispatchQuery(new GetHotelById.Query { Id = id }, ct);

        if (data is null)
        {
            return NotFound();
        }

        return Ok(data);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> Post([FromBody] Hotel hotel, CancellationToken ct)
    {
        var result = await _mediator.Send(
                        new CreateHotel(hotel.Name,
                                        hotel.StarsOfCategory,
                                        hotel.Street,
                                        hotel.District,
                                        hotel.City,
                                        hotel.Country,
                                        hotel.Zipcode,
                                        hotel.Email,
                                        hotel.Phone,
                                        hotel.Mobile), ct);

        await _transactionManager.CommitAsync(result, ct);

        if (result.IsFailed)
        {
            return UnprocessableEntity(result.Errors);
        }

        return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Value }, null);
    }

    [HttpPatch("{id:Guid}/address")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> PatchAddress([FromBody] PatchAddress value, CancellationToken ct)
    {
        var result = await _mediator.Send(new UpdateHotelAddress(
                                            value.HotelCode,
                                            value.Street,
                                            value.District,
                                            value.City,
                                            value.Country,
                                            value.Zipcode
                                        ), ct);

        await _transactionManager.CommitAsync(result, ct);

        if (result.IsFailed)
        {
            return UnprocessableEntity(result.Errors);
        }

        return NoContent();
    }

    [HttpPatch("{code:Guid}/contacts")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> PatchContacts([FromBody] PatchContacts value, CancellationToken ct)
    {
        var result = await _mediator.Send(new UpdateHotelContacts(
                                            value.HotelCode,
                                            value.Email,
                                            value.Phone,
                                            value.Mobile
                                        ), ct);

        await _transactionManager.CommitAsync(result, ct);

        if (result.IsFailed)
        {
            return UnprocessableEntity(result.Errors);
        }

        return NoContent();
    }

    [HttpPost("{code:Guid}/room")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> PostRoomAsync([FromBody] PostRoom room, CancellationToken ct)
    {
        var result = await _mediator.Send(
                        new AddRoomToHotel(
                            room.HotelCode,
                            room.Name,
                            room.Description,
                            room.Capacity,
                            room.AvailableQuantity,
                            room.PricePerNight,
                            room.Amenities), ct);

        await _transactionManager.CommitAsync(result, ct);

        if (result.IsFailed)
        {
            return UnprocessableEntity(result.Errors);
        }

        return CreatedAtAction(nameof(GetByIdAsync), new { id = room.HotelCode }, null);
    }

}
