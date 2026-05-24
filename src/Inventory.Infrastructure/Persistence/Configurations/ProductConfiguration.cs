using Maintrols.Companies.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Maintrols.Companies.Infrastructure.Persistence.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.HasKey(item => item.Id);
        builder.Property(item => item.Email).HasMaxLength(100).IsRequired();
        builder.Property(item => item.Name).HasMaxLength(100).IsRequired();
        builder.Property(item => item.ExpirationDate).IsRequired();
        builder.Property(item => item.LogoUrl).IsRequired();
        builder.Property(item => item.Status).IsRequired();
    }
}