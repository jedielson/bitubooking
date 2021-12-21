namespace BituBooking.Domain.Management.Handlers;

using System.Threading;



using BituBooking.Domain.Management.Commands;
using BituBooking.SharedKernell.Domain;

using FluentResults;

using MediatR;

public class UpdateHotelContactsHandler : ICommandHandler<UpdateHotelContacts, Result>
{
    private readonly IManagementRepository _hotelPersistence;

    public UpdateHotelContactsHandler(IManagementRepository hotelPersistence)
    {
        _hotelPersistence = hotelPersistence;
    }

    public async Task<Result> Handle(UpdateHotelContacts command, CancellationToken ct)
    {
        // TODO It needs to be analyzed to verify if it makes more sense send only Hotel Identifier and Address
        var hotel = await _hotelPersistence.RetrieveHotelByCodeAsync(command.HotelCode, ct);

        if (hotel is null)
            return Result.Fail("There isn't an hotel to update the Contacts");

        var contacts = Contacts.Create(command.NewPhone,
                                       command.NewMobile,
                                       command.NewEmail);

        hotel.ChangeContacts(contacts);
        return Result.Ok();
    }
}
