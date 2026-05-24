using Inventory.Domain.Entities;
using Inventory.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Infrastructure.Persistence.Configurations;

public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.ToTable("Invoices");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.Id)
            .ValueGeneratedNever();

        builder.Property(i => i.ClientId)
            .IsRequired();

        // Use decimal(18,4) for monetary values for accuracy
        builder.Property(i => i.TotalAmount)
            .HasColumnType("decimal(18,4)")
            .IsRequired();

        builder.Property(i => i.TotalDiscount)
            .HasColumnType("decimal(18,4)")
            .IsRequired();

        builder.Property(i => i.PaidAmount)
            .HasColumnType("decimal(18,4)")
            .IsRequired();

        // Computed properties are NOT persisted — they are calculated in-memory
        builder.Ignore(i => i.TotalAmountDiscounted);
        builder.Ignore(i => i.TotalRemaining);

        builder.Property(i => i.InvoiceType)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(i => i.CreatedAt)
            .IsRequired();

        // Relations
        builder.HasOne(i => i.Client)
            .WithMany()
            .HasForeignKey(i => i.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        builder.HasIndex(i => i.ClientId);
        builder.HasIndex(i => i.CreatedAt);
        builder.HasIndex(i => i.InvoiceType);
    }
}
