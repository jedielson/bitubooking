namespace BituBooking.Infra.Storage.Postgres.Repositories;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using BituBooking.Domain.Management;

using Microsoft.EntityFrameworkCore;

public class ManagementRepository : IManagementRepository
{
    private readonly BookingContext _context;

    public ManagementRepository(BookingContext context)
    {
        _context = context;
    }

    public Task<List<Hotel>> GetAll(CancellationToken ct)
    {
        return _context.Set<Hotel>().ToListAsync(ct);
    }

    public Task<Hotel?> RetrieveHotelByCodeAsync(Guid hotelCode, CancellationToken ct)
    {
        return _context.Set<Hotel>()
                .Include(x => x.Rooms)
                .FirstOrDefaultAsync(x => x.Code == hotelCode, ct);
    }

    public async Task SaveAsync(Hotel hotel, CancellationToken ct)
    {
        await _context.Set<Hotel>().AddAsync(hotel, ct);
    }

    public Task SaveRoomAsync(Hotel hotel, Room room, CancellationToken ct)
    {
        var r = hotel.Rooms.FirstOrDefault(x => x.Code == room.Code);

        if (r is null)
        {
            throw new Exception("Room does not exists");
        }

        _context.Entry(r).State = EntityState.Added;
        return Task.CompletedTask;
    }
}
