using Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Infrastructure.Persistence.Configurations;

public class InvoiceProductConfiguration : IEntityTypeConfiguration<InvoiceProduct>
{
    public void Configure(EntityTypeBuilder<InvoiceProduct> builder)
    {
        builder.ToTable("InvoiceProducts");

        builder.HasKey(ip => ip.Id);

        builder.Property(ip => ip.Id)
            .ValueGeneratedNever();

        builder.Property(ip => ip.InvoiceId)
            .IsRequired();

        builder.Property(ip => ip.ProductId)
            .IsRequired();

        builder.Property(ip => ip.StockProductId)
            .IsRequired();

        builder.Property(ip => ip.SellingPrice)
            .HasColumnType("decimal(18,4)")
            .IsRequired();

        builder.Property(ip => ip.ActualSellingPrice)
            .HasColumnType("decimal(18,4)")
            .IsRequired();

        builder.Property(ip => ip.CreatedAt)
            .IsRequired();

        // Relations
        // InvoiceProduct belongs to an Invoice (no nav property on Invoice side)
        builder.HasOne<Invoice>()
            .WithMany()
            .HasForeignKey(ip => ip.InvoiceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ip => ip.Product)
            .WithMany()
            .HasForeignKey(ip => ip.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ip => ip.StockProduct)
            .WithMany()
            .HasForeignKey(ip => ip.StockProductId)
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        builder.HasIndex(ip => ip.InvoiceId);
        builder.HasIndex(ip => ip.ProductId);

        // A stock product can only be sold once per invoice
        builder.HasIndex(ip => ip.StockProductId)
            .IsUnique();
    }
}
