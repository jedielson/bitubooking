namespace BituBooking.Infra.Storage.Mongo.Queries;

using System.Threading;
using System.Threading.Tasks;

using BituBooking.Infra.Storage.Mongo.Common;
using BituBooking.Reading.Queries;
using BituBooking.SharedKernell.Queries;

using MongoDB.Driver;

public class GetHotelByIdHandler : IQueryHandler<GetHotelById.Query, GetHotelById.Result?>
{
    private readonly MongoContext _context;

    public GetHotelByIdHandler(MongoContext context)
    {
        _context = context;
    }

    public async Task<GetHotelById.Result?> Handle(GetHotelById.Query request, CancellationToken ct)
    {
        var query = await _context.Hotels().FindAsync(x => x.Code == request.Id, null, ct);
        var data = query.FirstOrDefault(ct);
        if (data is null)
        {
            return null;
        }

        return new GetHotelById.Result
        {
            Code = data.Code,
            StarsOfCategory = data.StarsOfCategory,
            Name = data.Name,
            StarsOfRating = data.StarsOfRating,

            Email = data.Contacts?.Email ?? string.Empty,
            Mobile = data.Contacts?.Mobile ?? string.Empty,
            Phone = data.Contacts?.Phone ?? string.Empty,

            City = data.Address?.City ?? string.Empty,
            Country = data.Address?.Country ?? string.Empty,
            District = data.Address?.District ?? string.Empty,
            Street = data.Address?.Street ?? string.Empty,
            Zipcode = data.Address?.ZipCode ?? 0,

            Rooms = data.Rooms?.Select(x => new GetHotelById.RoomResult
            {
                Amenities = x.Amenities,
                AvailableQuantity = x.AvailableQuantity,
                Capacity = x.Capacity,
                Description = x.Description,
                Name = x.Name,
                PricePerNight = x.PricePerNight
            }) ?? Enumerable.Empty<GetHotelById.RoomResult>()
        };
    }
}
