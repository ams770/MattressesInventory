using Inventory.Domain.Common;
using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Persistence.Repositories;

public class InvoiceProductRepository(InventoryDbDbContext dbContext) 
    : BaseRepository<InvoiceProduct, Guid>(dbContext), IInvoiceProductRepository
{
    public override async Task<InvoiceProduct?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(ip => ip.Product)
            .Include(ip => ip.StockProduct)
            .FirstOrDefaultAsync(ip => ip.Id == id, cancellationToken);
    }

    public override async Task<PagedResult<InvoiceProduct>> GetAllAsync(PagedRequest request, CancellationToken cancellationToken = default)
    {
        IQueryable<InvoiceProduct> query = DbSet
            .Include(ip => ip.Product)
            .Include(ip => ip.StockProduct);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var searchTerm = request.SearchTerm.Trim().ToLower();
            query = query.Where(ip => ip.Product.Name.ToLower().Contains(searchTerm) || 
                                     ip.Product.Code.ToLower().Contains(searchTerm));
        }

        int totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<InvoiceProduct>(items, totalCount, request.PageNumber, request.PageSize);
    }
}
