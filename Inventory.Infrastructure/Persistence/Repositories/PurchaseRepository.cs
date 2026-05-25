using Inventory.Domain.Common;
using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Persistence.Repositories;

public class PurchaseRepository(InventoryDbDbContext dbContext) 
    : BaseRepository<Purchase, Guid>(dbContext), IPurchaseRepository
{
    public override async Task<Purchase?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(p => p.Vendor)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public override async Task<PagedResult<Purchase>> GetAllAsync(PagedRequest request, CancellationToken cancellationToken = default)
    {
        IQueryable<Purchase> query = DbSet.Include(p => p.Vendor);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var searchTerm = request.SearchTerm.Trim().ToLower();
            query = query.Where(p => p.Vendor.Name.ToLower().Contains(searchTerm));
        }

        int totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<Purchase>(items, totalCount, request.PageNumber, request.PageSize);
    }
}
