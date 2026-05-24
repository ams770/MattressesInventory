using Inventory.Application.Products;
using Inventory.Domain.Common;

namespace Inventory.Application.Services;

public interface IProductsService
{
    Task<Guid> CreateAsync(string code, string name, string barcode, bool isActive, string? description, string? imageUrl, CancellationToken ct = default);
    Task UpdateAsync(Guid id, string code, string name, string barcode, bool isActive, string? description, string? imageUrl, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
    Task<ProductDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<PagedResult<ProductDto>> GetAllAsync(string? searchTerm, int pageNumber = 1, int pageSize = 10, CancellationToken ct = default);
}