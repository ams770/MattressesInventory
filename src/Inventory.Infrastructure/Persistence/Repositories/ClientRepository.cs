using Inventory.Domain.Common;
using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Persistence.Repositories;

public class ClientRepository(InventoryDbDbContext dbContext) 
    : BaseRepository<Client, Guid>(dbContext), IClientRepository
{
    public override async Task<PagedResult<Client>> GetAllAsync(PagedRequest request, CancellationToken cancellationToken = default)
    {
        IQueryable<Client> query = DbSet;

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var searchTerm = request.SearchTerm.Trim().ToLower();
            query = query.Where(c => c.Name.ToLower().Contains(searchTerm) || c.PhoneNumber.ToLower().Contains(searchTerm));
        }

        int totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<Client>(items, totalCount, request.PageNumber, request.PageSize);
    }
}
