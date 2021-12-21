namespace BituBooking.Domain.Management.Handlers;

using System.Threading;



using BituBooking.Domain.Management.Commands;
using BituBooking.SharedKernell.Domain;

using FluentResults;

public class CreateHotelHandler : ICommandHandler<CreateHotel, Result<Guid>>
{
    private readonly IManagementRepository _hotelPersistence;

    public CreateHotelHandler(IManagementRepository hotelPersistence)
    {
        _hotelPersistence = hotelPersistence;
    }

    public async Task<Result<Guid>> Handle(CreateHotel command, CancellationToken ct)
    {

        var address = Address.Create(command.Street,
                                     command.District,
                                     command.City,
                                     command.Country,
                                     command.Zipcode);

        var contacts = Contacts.Create(command.Phone,
                                             command.Mobile,
                                             command.Email);


        var hotel = new Hotel(command.Name,
                              command.StarsOfCategory,
                              address,
                              contacts);

        await _hotelPersistence.SaveAsync(hotel, ct);
        return Result.Ok(hotel.Code);
    }
}
