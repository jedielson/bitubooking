namespace BituBooking.Infra.Storage.Postgres.Mappings;

using BituBooking.Domain.Management;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class HotelMap : IEntityTypeConfiguration<Hotel>
{
    public void Configure(EntityTypeBuilder<Hotel> builder)
    {
        builder.HasKey(x => x.Identifier);
        builder.Property(x => x.Identifier);

        builder.Property(x => x.Name)
            .HasMaxLength(100);

        builder.Property(x => x.Code);


        builder.Property(x => x.StarsOfCategory)
                .HasDefaultValue(0);

        builder.Property(x => x.StarsOfRating)
            .HasDefaultValue(0);

        builder.OwnsOne(x => x.Address, nb =>
        {
            nb.Property(p => p.Street).HasMaxLength(100);
            nb.Property(p => p.District).HasMaxLength(100);
            nb.Property(p => p.City).HasMaxLength(100);
            nb.Property(p => p.Country).HasMaxLength(100);
            nb.Property(p => p.ZipCode);
        });


        builder.OwnsOne(x => x.Contacts, nb =>
        {
            nb.Property(p => p.Email).HasMaxLength(100);
            nb.Property(p => p.Mobile).HasMaxLength(100);
            nb.Property(p => p.Phone).HasMaxLength(50);
        });

        builder.HasMany(x => x.Rooms)
            .WithOne(x => x.Hotel)
            .HasForeignKey(x => x.HotelId);
    }
}
