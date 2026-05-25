using Inventory.Domain.Common;
using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Persistence.Repositories;

public class CategoryRepository(InventoryDbDbContext dbContext) 
    : BaseRepository<Category, Guid>(dbContext), ICategoryRepository
{
    public override async Task<PagedResult<Category>> GetAllAsync(PagedRequest request, CancellationToken cancellationToken = default)
    {
        IQueryable<Category> query = DbSet;

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var searchTerm = request.SearchTerm.Trim().ToLower();
            query = query.Where(c => c.Name.ToLower().Contains(searchTerm) || 
                                     (c.Description != null && c.Description.ToLower().Contains(searchTerm)));
        }

        int totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<Category>(items, totalCount, request.PageNumber, request.PageSize);
    }
}
