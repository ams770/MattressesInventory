using Inventory.Domain.Common;
using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Persistence.Repositories;

public class LocationPointRepository(InventoryDbDbContext dbContext) 
    : BaseRepository<LocationPoint, Guid>(dbContext), ILocationPointRepository
{
    public override async Task<PagedResult<LocationPoint>> GetAllAsync(PagedRequest request, CancellationToken cancellationToken = default)
    {
        IQueryable<LocationPoint> query = DbSet;

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var searchTerm = request.SearchTerm.Trim().ToLower();
            query = query.Where(lp => lp.Name.ToLower().Contains(searchTerm));
        }

        int totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<LocationPoint>(items, totalCount, request.PageNumber, request.PageSize);
    }
}
