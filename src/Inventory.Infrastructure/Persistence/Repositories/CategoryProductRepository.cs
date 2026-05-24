using Inventory.Domain.Common;
using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Persistence.Repositories;

public class CategoryProductRepository(InventoryDbDbContext dbContext) 
    : BaseRepository<CategoryProduct, Guid>(dbContext), ICategoryProductRepository
{
    public override async Task<CategoryProduct?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(cp => cp.Category)
            .Include(cp => cp.Product)
            .FirstOrDefaultAsync(cp => cp.Id == id, cancellationToken);
    }

    public override async Task<PagedResult<CategoryProduct>> GetAllAsync(PagedRequest request, CancellationToken cancellationToken = default)
    {
        IQueryable<CategoryProduct> query = DbSet
            .Include(cp => cp.Category)
            .Include(cp => cp.Product);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var searchTerm = request.SearchTerm.Trim().ToLower();
            query = query.Where(cp => cp.Category.Name.ToLower().Contains(searchTerm) || 
                                     cp.Product.Name.ToLower().Contains(searchTerm));
        }

        int totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<CategoryProduct>(items, totalCount, request.PageNumber, request.PageSize);
    }
}
