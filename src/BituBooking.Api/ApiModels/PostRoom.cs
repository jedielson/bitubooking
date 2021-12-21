#nullable disable
namespace BituBooking.Api.ApiModels;

using Microsoft.AspNetCore.Mvc;

public class PostRoom
{
    [FromRoute(Name = "code")]
    public Guid HotelCode { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Capacity { get; set; }
    public int AvailableQuantity { get; set; }
    public decimal PricePerNight { get; set; }
    public IList<string> Amenities { get; set; }
}
