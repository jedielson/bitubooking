namespace BituBooking.Infra.Storage.Mongo.Documents;

using MongoDB.Bson.Serialization.Attributes;

public class Address
{
    public string? Street { get; set; }
    public string? District { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public int ZipCode { get; set; }
}

public class Contacts
{
    public string? Mobile { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
}

public class Room
{
    public Room()
    {
        Amenities = new();
    }
    public Guid Code { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int Capacity { get; set; }
    public int AvailableQuantity { get; set; }
    public decimal PricePerNight { get; set; }
    public List<string> Amenities { get; set; }
}

public class Hotel
{
    public Hotel()
    {
        Rooms = new();
    }

    [BsonId]
    public Guid Id { get; set; }
    public Guid Code { get; set; }
    public string? Name { get; set; }
    public int StarsOfCategory { get; set; }
    public int StarsOfRating { get; set; }
    public Address? Address { get; set; }
    public Contacts? Contacts { get; set; }
    public List<Room>? Rooms { get; set; }
}
