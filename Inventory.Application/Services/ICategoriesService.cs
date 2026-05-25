using Inventory.Application.Products;
using Inventory.Domain.Common;

namespace Inventory.Application.Services;

public interface ICategoriesService
{
    Task<Guid> CreateAsync(string name, string? imageUrl, string? description, bool isActive, CancellationToken ct = default);
    Task UpdateAsync(Guid id, string name, string? imageUrl, string? description, bool isActive, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
    Task<CategoryDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<PagedResult<CategoryDto>> GetAllAsync(string? searchTerm, int pageNumber = 1, int pageSize = 10, CancellationToken ct = default);
}