namespace BituBooking.Infra.Storage.Postgres;

using Microsoft.EntityFrameworkCore;

public class BookingContext : DbContext
{
    public BookingContext(DbContextOptions options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("public");

        modelBuilder
        .ApplyConfigurationsFromAssembly(typeof(BookingContext).Assembly);
    }
}
