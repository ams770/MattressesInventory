using Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Infrastructure.Persistence.Configurations;

public class CategoryProductConfiguration : IEntityTypeConfiguration<CategoryProduct>
{
    public void Configure(EntityTypeBuilder<CategoryProduct> builder)
    {
        builder.ToTable("CategoryProducts");

        builder.HasKey(cp => cp.Id);

        builder.Property(cp => cp.Id)
            .ValueGeneratedNever();

        builder.Property(cp => cp.CategoryId)
            .IsRequired();

        builder.Property(cp => cp.ProductId)
            .IsRequired();

        builder.Property(cp => cp.CreatedAt)
            .IsRequired();

        // Relations
        builder.HasOne(cp => cp.Category)
            .WithMany()
            .HasForeignKey(cp => cp.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(cp => cp.Product)
            .WithMany()
            .HasForeignKey(cp => cp.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        // Prevent duplicate category-product pairs
        builder.HasIndex(cp => new { cp.CategoryId, cp.ProductId })
            .IsUnique();
    }
}
