namespace BituBooking.Infra.Storage.Postgres.Mappings;

using BituBooking.Domain.Management;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class RoomMap : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.HasKey(x => x.Identifier);

        builder.Property(x => x.Identifier);

        builder.Property(x => x.Code);
        builder.Property(x => x.Name).HasMaxLength(30);
        builder.Property(x => x.Description).HasMaxLength(300);
        builder.Property(x => x.Capacity);
        builder.Property(x => x.AvailableQuantity);
        builder.Property(x => x.PricePerNight);

        builder.HasOne(x => x.Hotel)
            .WithMany(x => x.Rooms)
            .HasForeignKey(x => x.HotelId);
    }
}
