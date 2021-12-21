namespace BituBooking.Domain.Management.Handlers;

using System.Threading;



using BituBooking.Domain.Management.Commands;
using BituBooking.SharedKernell.Domain;

using FluentResults;

using MediatR;

public class UpdateHotelAddressHandler : ICommandHandler<UpdateHotelAddress, Result>
{
    private readonly IManagementRepository _hotelPersistence;

    public UpdateHotelAddressHandler(IManagementRepository hotelPersistence)
    {
        _hotelPersistence = hotelPersistence;
    }

    public async Task<Result> Handle(UpdateHotelAddress command, CancellationToken ct)
    {
        //TODO It needs to be analyzed to verify if it makes more sense send only Hotel Identifier and Address
        var hotel = await _hotelPersistence.RetrieveHotelByCodeAsync(command.HotelCode, ct);

        if (hotel is null)
            return Result.Fail("There isn't an hotel to update the Address");

        var address = Address.Create(command.NewStreet,
                                     command.NewDistrict,
                                     command.NewCity,
                                     command.NewCountry,
                                     command.NewZipcode);

        hotel.ChangeAddress(address);
        return Result.Ok();
    }
}
