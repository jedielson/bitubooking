namespace BituBooking.Domain.Management.Handlers;

using System.Threading;



using BituBooking.Domain.Management.Commands;
using BituBooking.SharedKernell.Domain;

using FluentResults;

using MediatR;

public class AddRoomToHotelHandler : ICommandHandler<AddRoomToHotel, Result>
{
    private readonly IManagementRepository _hotelPersistence;

    public AddRoomToHotelHandler(IManagementRepository hotelPersistence)
    {
        _hotelPersistence = hotelPersistence;
    }

    public async Task<Result> Handle(AddRoomToHotel command, CancellationToken ct)
    {
        try
        {
            // TODO Verify why Amenities aren't being loaded while loading a Room
            var searchedHotel = await _hotelPersistence.RetrieveHotelByCodeAsync(command.HotelCode, ct);

            if (searchedHotel is null)
                return Result.Fail("There isn't an hotel to add a new room");

            var room = new Room(command.Name,
                                command.Description,
                                command.Capacity,
                                command.AvailableQuantity,
                                command.PricePerNight,
                                searchedHotel);

            foreach (var amenity in command.Amenities)
            {
                room.AddAmenities(amenity);
            }

            searchedHotel.AddRoom(room);
            await _hotelPersistence.SaveRoomAsync(searchedHotel, room, ct);
            return Result.Ok();
        }
        catch (Exception)
        {
            return Result.Fail($"Error while creating a hotel");
        }
    }
}
