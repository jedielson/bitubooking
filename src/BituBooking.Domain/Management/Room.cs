namespace BituBooking.Domain.Management;

using System.Collections.Generic;

using BituBooking.SharedKernell.Domain;

public class Room : Entity
{
    private readonly IList<string> _amenities;

#pragma warning disable CS8618
    private Room()
#pragma warning restore CS8618
        : base(default)
    {
        _amenities = new List<string>();
    }

    public Room(string name, string description, int capacity,
        int availableQuantity, decimal pricePerNight, Hotel hotel, Guid? code = null)
        : base(Guid.NewGuid())
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidOperationException($"The room's {nameof(name)} MUST be filled");

        Code = code ?? Guid.NewGuid();

        Name = name;
        Description = description;
        Capacity = capacity;
        AvailableQuantity = availableQuantity;
        PricePerNight = pricePerNight;

        HotelId = hotel.Identifier;
        Hotel = hotel;

        _amenities = new List<string>();
    }

    public Guid Code { get; protected set; }
    public string Name { get; protected set; }
    public string Description { get; protected set; }
    public int Capacity { get; protected set; }
    public int AvailableQuantity { get; protected set; }
    public decimal PricePerNight { get; protected set; }

    public Guid HotelId { get; set; }

    public Hotel Hotel { get; set; }
    public IReadOnlyList<string> Amenities => _amenities.ToList();

    public void AddAmenities(string amenity)
    {
        // TODO review this implementation to invalidate this operation returning an error list instead of raise an exception
        if (_amenities.Contains(amenity))
            throw new InvalidOperationException($"The amenity {amenity} was already added.");

        _amenities.Add(amenity);
    }
}
