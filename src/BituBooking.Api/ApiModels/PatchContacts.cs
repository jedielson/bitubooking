#nullable disable
namespace BituBooking.Api.ApiModels;

using Microsoft.AspNetCore.Mvc;

public class PatchContacts
{
    [FromRoute(Name = "code")]
    public Guid HotelCode { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Mobile { get; set; }
}
