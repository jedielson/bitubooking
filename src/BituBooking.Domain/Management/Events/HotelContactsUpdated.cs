namespace BituBooking.Domain.Management.Events;

using BituBooking.SharedKernell.Domain;

public sealed class HotelContactsUpdated : IDomainEvent
{
    public Guid HotelCode { get; }
    public string Email { get; }
    public string Phone { get; }
    public string Mobile { get; }

    public HotelContactsUpdated(Guid hotelCode, string email, string phone,
        string mobile)
    {
        HotelCode = hotelCode;
        Email = email;
        Phone = phone;
        this.Mobile = mobile;
    }
}

