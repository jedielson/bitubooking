namespace BituBooking.Reading.Queries;

using BituBooking.SharedKernell.Queries;

public class GetHotelById
{
    public class Query : IQuery<Query, Result?>
    {
        public Guid Id { get; set; }
    }

    public class Result
    {
        public Guid Code { get; set; }
        public string? Name { get; set; }
        public int StarsOfCategory { get; set; }
        public int StarsOfRating { get; set; }
        public string? Street { get; set; }
        public string? District { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public int Zipcode { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public IEnumerable<RoomResult>? Rooms { get; set; }
    }

    public class RoomResult
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Capacity { get; set; }
        public int AvailableQuantity { get; set; }
        public decimal PricePerNight { get; set; }
        public IEnumerable<string>? Amenities { get; set; }
    }
}
