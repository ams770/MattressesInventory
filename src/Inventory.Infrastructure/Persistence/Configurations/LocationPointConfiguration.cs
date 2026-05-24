using Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Infrastructure.Persistence.Configurations;

public class LocationPointConfiguration : IEntityTypeConfiguration<LocationPoint>
{
    public void Configure(EntityTypeBuilder<LocationPoint> builder)
    {
        builder.ToTable("LocationPoints");

        builder.HasKey(lp => lp.Id);

        builder.Property(lp => lp.Id)
            .ValueGeneratedNever();

        builder.Property(lp => lp.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(lp => lp.CreatedAt)
            .IsRequired();

        // Indexes
        builder.HasIndex(lp => lp.Name)
            .IsUnique();
    }
}
