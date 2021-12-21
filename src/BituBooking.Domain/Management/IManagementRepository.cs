namespace BituBooking.Domain.Management;

using System.Threading.Tasks;

public interface IManagementRepository
{
    // Task CreateHotelAsync(Hotel hotel);
    // Task UpdateHotelAddress(Hotel hotel);
    // Task UpdateHotelContacts(Hotel hotel);
    // Task AddRoomToHotel(Hotel hotel);
    Task SaveAsync(Hotel hotel, CancellationToken ct);

    Task<Hotel?> RetrieveHotelByCodeAsync(Guid hotelCode, CancellationToken ct);

    Task<List<Hotel>> GetAll(CancellationToken ct);

    Task SaveRoomAsync(Hotel hotel, Room room, CancellationToken ct);
}
