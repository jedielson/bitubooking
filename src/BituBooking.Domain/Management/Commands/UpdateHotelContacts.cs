namespace BituBooking.Domain.Management.Commands;

using BituBooking.SharedKernell.Domain;

public class UpdateHotelContacts : ICommand
{
    public Guid HotelCode { get; }
    public string NewEmail { get; }
    public string NewPhone { get; }
    public string NewMobile { get; }

    public UpdateHotelContacts(Guid hotelCode, string newEmail,
        string newPhone, string newMobile)
    {
        HotelCode = hotelCode;
        NewEmail = newEmail;
        NewPhone = newPhone;
        NewMobile = newMobile;
    }
}
