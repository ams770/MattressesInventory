using Inventory.Domain.Entities;
using Inventory.Domain.Common;

namespace Inventory.Domain.Interfaces;

public interface IStockProductRepository : IRepository<StockProduct, Guid>
{
    Task<PagedResult<StockProduct>> GetAllAsync(StockProductPagedRequest request, CancellationToken cancellationToken = default);
}
