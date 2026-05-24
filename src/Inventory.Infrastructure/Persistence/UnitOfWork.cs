using Inventory.Application.Interfaces;

namespace Inventory.Infrastructure.Persistence;

public class UnitOfWork(InventoryDbDbContext dbContext) : IUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken ct = default)
    {
        await dbContext.SaveChangesAsync(ct);
    }
}