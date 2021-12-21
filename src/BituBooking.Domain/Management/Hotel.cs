namespace BituBooking.Domain.Management;

using System.Collections.Generic;

using BituBooking.Domain.Management.Events;
using BituBooking.SharedKernell.Domain;

public class Hotel : Aggregate
{
    protected Hotel() : base(default)
    {
        Rooms = new List<Room>();
        Name = string.Empty;
        Address = Address.None;
        Contacts = Contacts.None;
    }

    public Hotel(string name, int starsOfCategory, Address address,
        Contacts contacts, Guid code = default, Guid? identifier = null)
            : base(identifier)
    {
        Name = name;
        StarsOfCategory = starsOfCategory;
        StarsOfRating = 0;
        Address = address;
        Contacts = contacts;

        Rooms = new List<Room>();

        Code = code;

        if (Code == default)
        {
            Code = Guid.NewGuid();

            AddEvent(new HotelCreated(Code,
                Name,
                StarsOfCategory,
                StarsOfRating,
                Address,
                Contacts));
        };
    }

    public Hotel(Guid code, int starsOfCategory, Address address)
        : base(Guid.NewGuid())
    {
        Code = code;
        StarsOfCategory = starsOfCategory;
        Address = address;
        Rooms = new List<Room>();
        Name = "";
        Address = Address.None;
        Contacts = Contacts.None;
    }

    public Guid Code { get; }
    public string Name { get; }
    public int StarsOfCategory { get; }
    public int StarsOfRating { get; }
    public Address Address { get; private set; }
    public Contacts Contacts { get; private set; }

    public virtual ICollection<Room> Rooms { get; protected set; }

    public void ChangeAddress(Address address)
    {
        Address = address;

        AddEvent(new HotelAddressUpdated(this.Code,
                                         address.Street,
                                         address.District,
                                         address.City,
                                         address.Country,
                                         address.ZipCode));
    }

    public void ChangeContacts(Contacts contacts)
    {
        Contacts = contacts;

        AddEvent(new HotelContactsUpdated(this.Code,
                                          contacts.Email,
                                          contacts.Phone,
                                          contacts.Mobile));
    }

    internal void AddRoom(Room room)
    {
        if (Rooms.Contains(room))
            throw new InvalidOperationException($"The room {room.Name} already was added.");

        room.HotelId = Identifier;
        room.Hotel = this;

        Rooms.Add(room);

        AddEvent(new RoomAdded(room.Code,
                               this.Code,
                               room.Name,
                               room.Description,
                               room.PricePerNight,
                               room.Capacity,
                               room.AvailableQuantity,
                               room.Amenities));
    }

    internal void RemoveRoom(Room room)
    {
        if (!Rooms.Contains(room))
            throw new InvalidOperationException($"The selected room {room.Name} doesn't exists to be removed.");

        Rooms.Remove(room);

        AddEvent(new RoomRemoved());
    }
}
