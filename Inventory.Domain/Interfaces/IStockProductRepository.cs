using Inventory.Domain.Entities;
using Inventory.Domain.Common;
using Inventory.Domain.Enums;

namespace Inventory.Domain.Interfaces;

public interface IStockProductRepository : IRepository<StockProduct, Guid>
{
    Task<PagedResult<StockProduct>> GetAllAsync(StockProductPagedRequest request, CancellationToken cancellationToken = default);
    Task<int> GetCountByProductIdAndStatusAsync(Guid productId, StockProductStatus status, CancellationToken cancellationToken = default);
    Task<List<StockProduct>> GetAvailableProductsAsync(Guid productId, int count, CancellationToken cancellationToken = default);
}
