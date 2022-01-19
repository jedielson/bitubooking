namespace BituBooking.Integration.Tests.Hotel;

using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;

using BituBooking.Reading.Queries;

public class HotelTests : TestBase
{
    public HotelTests(IntegrationTestFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task Hotel_Should_Be_Created()
    {
        // Arrange
        var request = new AutoFaker<Api.ApiModels.Hotel>().Generate();

        // Act
        var result = await PostAsync("hotel", request);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
        await AssertLocation<GetHotelById.Result>(result.Location, data =>
        {
            data.Should().BeEquivalentTo(request);
        });

    }

    // [Fact]
    // public async Task Address_Should_Be_Updated()
    // {
    //     // Arrange
    //     var request = new AutoFaker<Api.ApiModels.Hotel>().Generate();
    //     var createdHotel = await PostAsync("hotel", request);
    //     var patchRequest = new AutoFaker<Api.ApiModels.PatchAddress>()
    //                             .RuleFor(x => x.Street, f => f.Address.StreetAddress())
    //                             .RuleFor(x => x.City, f => f.Address.City())
    //                             .RuleFor(x => x.Country, f => f.Address.Country())
    //                             .RuleFor(x => x.District, f => f.Address.City())
    //                             .Generate();

    //     patchRequest.HotelCode = Guid.Parse(createdHotel.Location.Split('/').Last());
    //     var content = new StringContent(JsonSerializer.Serialize(patchRequest), Encoding.UTF8, "application/json");

    //     // Act
    //     var result = await Client.PatchAsync($"hotel/{patchRequest.HotelCode}/address", content);

    //     // Assert
    //     result.StatusCode.Should().Be(HttpStatusCode.NoContent);

    //     await AssertLocation<GetHotelById.Result>(createdHotel.Location, data =>
    //     {
    //         data.City.Should().Be(patchRequest.City);
    //         data.Street.Should().Be(patchRequest.Street);
    //         data.Country.Should().Be(patchRequest.Country);
    //         data.District.Should().Be(patchRequest.District);
    //         data.Zipcode.Should().Be(patchRequest.Zipcode);
    //     });
    // }

    //[Fact]
    // public async Task Contacts_Should_Be_Updated()
    // {
    //     // Arrange
    //     var request = new AutoFaker<Api.ApiModels.Hotel>().Generate();
    //     var createdHotel = await PostAsync("hotel", request);
    //     var patchRequest = new AutoFaker<Api.ApiModels.PatchContacts>()
    //                             .RuleFor(x => x.Email, f => f.Internet.Email())
    //                             .RuleFor(x => x.Phone, f => f.Phone.PhoneNumber())
    //                             .RuleFor(x => x.Mobile, f => f.Phone.PhoneNumber())
    //                             .Generate();

    //     patchRequest.HotelCode = Guid.Parse(createdHotel.Location.Split('/').Last());
    //     var content = new StringContent(JsonSerializer.Serialize(patchRequest), Encoding.UTF8, "application/json");

    //     // Act
    //     var result = await Client.PatchAsync($"hotel/{patchRequest.HotelCode}/contacts", content);

    //     // Assert
    //     result.StatusCode.Should().Be(HttpStatusCode.NoContent);

    //     await AssertLocation<GetHotelById.Result>(createdHotel.Location, data =>
    //     {
    //         data.Email.Should().Be(patchRequest.Email);
    //         data.Phone.Should().Be(patchRequest.Phone);
    //         data.Mobile.Should().Be(patchRequest.Mobile);
    //     });
    // }

    // [Fact]
    // public async Task Room_Should_Be_Added()
    // {
    //     // Arrange
    //     var request = new AutoFaker<Api.ApiModels.Hotel>().Generate();
    //     var createdHotel = await PostAsync("hotel", request);
    //     var roomRequest = new AutoFaker<Api.ApiModels.PostRoom>()
    //                             .Generate();

    //     roomRequest.HotelCode = Guid.Parse(createdHotel.Location.Split('/').Last());

    //     // Act
    //     var result = await PostAsync($"hotel/{roomRequest.HotelCode}/room", roomRequest);

    //     // Assert
    //     result.StatusCode.Should().Be(HttpStatusCode.Created);

    //     await AssertLocation<GetHotelById.Result>(createdHotel.Location, data =>
    //     {
    //         data.Rooms.Should().BeEquivalentTo(new[] { roomRequest }, opt =>
    //             opt.Excluding(x => x.HotelCode));
    //     });
    // }
}
