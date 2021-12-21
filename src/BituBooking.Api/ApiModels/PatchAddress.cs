#nullable disable
namespace BituBooking.Api.ApiModels;

using Microsoft.AspNetCore.Mvc;

public class PatchAddress
{
    [FromRoute(Name = "code")]
    public Guid HotelCode { get; set; }
    public string Street { get; set; }
    public string District { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public int Zipcode { get; set; }

}
