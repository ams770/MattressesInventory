using Inventory.Domain.Common;
using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Persistence.Repositories;

public class StockProductRepository(InventoryDbDbContext dbContext) 
    : BaseRepository<StockProduct, Guid>(dbContext), IStockProductRepository
{
    public override async Task<StockProduct?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(s => s.Product)
            .Include(s => s.Purchase)
            .Include(s => s.LocationPoint)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public override async Task<PagedResult<StockProduct>> GetAllAsync(PagedRequest request, CancellationToken cancellationToken = default)
    {
        IQueryable<StockProduct> query = DbSet
            .Include(s => s.Product)
            .Include(s => s.Purchase)
            .Include(s => s.LocationPoint);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var searchTerm = request.SearchTerm.Trim().ToLower();
            query = query.Where(s => s.Product.Name.ToLower().Contains(searchTerm));
        }

        int totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<StockProduct>(items, totalCount, request.PageNumber, request.PageSize);
    }

    public async Task<PagedResult<StockProduct>> GetAllAsync(StockProductPagedRequest request, CancellationToken cancellationToken = default)
    {
        IQueryable<StockProduct> query = DbSet
            .Include(s => s.Product)
            .Include(s => s.Purchase)
            .Include(s => s.LocationPoint);

        if (request.Status.HasValue)
        {
            query = query.Where(s => s.Status == request.Status.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var searchTerm = request.SearchTerm.Trim().ToLower();
            query = query.Where(s => s.Product.Name.ToLower().Contains(searchTerm));
        }

        int totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<StockProduct>(items, totalCount, request.PageNumber, request.PageSize);
    }
}
