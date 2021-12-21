namespace BituBooking.Infra.Kafka;

public static class Constants
{
    public class Topics
    {
        public const string HotelContactsUpdated = "hotel-contacts-updated";
        public const string HotelCreated = "hotel-created";
        public const string HotelAddressUpdated = "hotel-address-updated";
        public const string RoomAdded = "room-added";
    }
}
