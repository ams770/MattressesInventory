using Inventory.Domain.Common;
using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Persistence.Repositories;

public class InvoiceRepository(InventoryDbDbContext dbContext) 
    : BaseRepository<Invoice, Guid>(dbContext), IInvoiceRepository
{
    public override async Task<Invoice?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(i => i.Client)
            .FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
    }

    public override async Task<PagedResult<Invoice>> GetAllAsync(PagedRequest request, CancellationToken cancellationToken = default)
    {
        IQueryable<Invoice> query = DbSet.Include(i => i.Client);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var searchTerm = request.SearchTerm.Trim().ToLower();
            query = query.Where(i => i.Client.Name.ToLower().Contains(searchTerm) || 
                                     i.Client.PhoneNumber.ToLower().Contains(searchTerm));
        }

        int totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<Invoice>(items, totalCount, request.PageNumber, request.PageSize);
    }

    public async Task<PagedResult<Invoice>> GetAllAsync(InvoicePagedRequest request, CancellationToken cancellationToken = default)
    {
        IQueryable<Invoice> query = DbSet.Include(i => i.Client);

        if (request.ClientId.HasValue && request.ClientId.Value != Guid.Empty)
        {
            query = query.Where(i => i.ClientId == request.ClientId.Value);
        }

        if (request.FromDate.HasValue)
        {
            query = query.Where(i => i.CreatedAt >= request.FromDate.Value);
        }

        if (request.ToDate.HasValue)
        {
            query = query.Where(i => i.CreatedAt <= request.ToDate.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var searchTerm = request.SearchTerm.Trim().ToLower();
            query = query.Where(i => i.Client.Name.ToLower().Contains(searchTerm) || 
                                     i.Client.PhoneNumber.ToLower().Contains(searchTerm));
        }

        int totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<Invoice>(items, totalCount, request.PageNumber, request.PageSize);
    }
}
