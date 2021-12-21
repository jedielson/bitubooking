namespace BituBooking.Infra.Storage.Mongo.Services;

using BituBooking.Infra.Storage.Mongo.Common;
using BituBooking.Infra.Storage.Mongo.Documents;

using MongoDB.Driver;

public interface IHotelService
{
    Task<Hotel> GetByCodeAsync(Guid code, CancellationToken ct);
    Task UpdateAddressAsync(Guid code, Address address, CancellationToken ct);
    Task UpdateContactsAsync(Guid code, Contacts contacts, CancellationToken ct);
    Task AddRoomAsync(Guid code, Room room, CancellationToken ct);
    Task Save(Hotel hotel, CancellationToken ct);
}

public class HotelService : IHotelService
{
    private readonly MongoContext _context;

    public HotelService(MongoContext context)
    {
        _context = context;
    }

    public async Task AddRoomAsync(Guid code, Room room, CancellationToken ct)
    {
        var filter = Builders<Hotel>.Filter.Eq(x => x.Code, code);
        var update = Builders<Hotel>.Update.Push(x => x.Rooms, room);
        var result = await _context.Hotels().UpdateOneAsync(filter, update, null, ct);
    }

    public async Task<Hotel> GetByCodeAsync(Guid code, CancellationToken ct)
    {
        var query = await _context.Hotels().FindAsync(x => x.Code == code, null, ct);
        var data = query.FirstOrDefault(ct);
        return data;
    }

    public Task Save(Hotel hotel, CancellationToken ct)
    {
        return _context.Hotels().InsertOneAsync(hotel, new InsertOneOptions(), ct);
    }

    public async Task UpdateAddressAsync(Guid code, Address address, CancellationToken ct)
    {
        var filter = Builders<Hotel>.Filter.Eq(x => x.Code, code);
        var update = Builders<Hotel>.Update.Set(x => x.Address, address);
        var result = await _context.Hotels().UpdateOneAsync(filter, update, null, ct);
    }

    public async Task UpdateContactsAsync(Guid code, Contacts contacts, CancellationToken ct)
    {
        var filter = Builders<Hotel>.Filter.Eq(x => x.Code, code);
        var update = Builders<Hotel>.Update.Set(x => x.Contacts, contacts);
        var result = await _context.Hotels().UpdateOneAsync(filter, update, null, ct);
    }
}