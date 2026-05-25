using Inventory.Domain.Common;
using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Persistence.Repositories;

public class ProductRepository(InventoryDbDbContext dbContext) 
    : BaseRepository<Product, Guid>(dbContext), IProductRepository
{
    public override async Task<PagedResult<Product>> GetAllAsync(PagedRequest request, CancellationToken cancellationToken = default)
    {
        IQueryable<Product> query = DbSet;

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var searchTerm = request.SearchTerm.Trim().ToLower();
            query = query.Where(p => p.Name.ToLower().Contains(searchTerm) || 
                                     p.Code.ToLower().Contains(searchTerm) || 
                                     p.Barcode.ToLower().Contains(searchTerm) || 
                                     (p.Description != null && p.Description.ToLower().Contains(searchTerm)));
        }

        int totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<Product>(items, totalCount, request.PageNumber, request.PageSize);
    }
}
