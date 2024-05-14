using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SerilogAPI.Entities;

namespace SerilogAPI.Data.EntitiesMapping;

internal sealed class PersonMapping : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
               .HasColumnName("name")
               .HasColumnType("varchar(100)")
               .IsRequired(true);

        builder.Property(p => p.Age)
               .HasColumnName("age")
               .HasColumnType("integer")
               .IsRequired(true);
    }
}
