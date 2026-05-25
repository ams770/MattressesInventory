using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Persistence;

public class InventoryDbDbContext(DbContextOptions<InventoryDbDbContext> options) : DbContext(options)
{
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //------ Auto-discovers all IEntityTypeConfiguration<T> ------//
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(InventoryDbDbContext).Assembly);
    }

}