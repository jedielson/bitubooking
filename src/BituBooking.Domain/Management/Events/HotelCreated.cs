namespace BituBooking.Domain.Management.Events;

using System.Text.Json.Serialization;

using BituBooking.SharedKernell.Domain;

public sealed class HotelCreated : IDomainEvent
{
    public Guid Code { get; set; }
    public string Name { get; set; }
    public int StarsOfCategory { get; set; }
    public int StarsOfRating { get; set; }
    public Address Address { get; set; }
    public Contacts Contacts { get; set; }

    [JsonConstructor]
    public HotelCreated(Guid code, string name, int starsOfCategory, int starsOfRating,
        Address address, Contacts contacts)
    {
        Code = code;
        Name = name;
        StarsOfCategory = starsOfCategory;
        StarsOfRating = starsOfRating;

        Address = address;
        Contacts = contacts;
    }
}

