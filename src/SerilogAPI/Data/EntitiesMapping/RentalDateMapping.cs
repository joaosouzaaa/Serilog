using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SerilogAPI.Entities;

namespace SerilogAPI.Data.EntitiesMapping;

internal sealed class RentalDateMapping : IEntityTypeConfiguration<RentalDate>
{
    public void Configure(EntityTypeBuilder<RentalDate> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.StartTime)
               .HasColumnName("start_time")
               .HasColumnType("timestamp without time zone")
               .IsRequired(true);

        builder.Property(r => r.EndTime)
               .HasColumnName("end_time")
               .HasColumnType("timestamp without time zone")
               .IsRequired(true);
    }
}
