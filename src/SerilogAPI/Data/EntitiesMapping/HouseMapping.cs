using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SerilogAPI.Entities;

namespace SerilogAPI.Data.EntitiesMapping;

internal sealed class HouseMapping : IEntityTypeConfiguration<House>
{
    public void Configure(EntityTypeBuilder<House> builder)
    {
        builder.HasKey(h => h.Id);

        builder.Property(h => h.Address)
               .HasColumnName("address")
               .HasColumnType("varchar(200)")
               .IsRequired(true);

        builder.Property(h => h.Price)
               .HasColumnName("price")
               .HasColumnType("decimal(18, 2)")
               .IsRequired(true);

        builder.Property(h => h.Number)
               .HasColumnName("number")
               .HasColumnType("integer")
               .IsRequired(true);

        builder.Property(h => h.NumberOfRooms)
               .HasColumnName("number_of_rooms")
               .HasColumnType("integer")
               .IsRequired(true);

        builder.HasOne(h => h.Owner)
               .WithMany(p => p.Houses)
               .HasConstraintName("FK_Owner_House")
               .HasForeignKey(h => h.OwnerId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(h => h.RentalDates)
               .WithOne(r => r.House)
               .HasConstraintName("FK_RentalDate_House")
               .HasForeignKey(r => r.HouseId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
