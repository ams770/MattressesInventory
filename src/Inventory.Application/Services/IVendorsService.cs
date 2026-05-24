using Inventory.Application.Vendors;
using Inventory.Domain.Common;

namespace Inventory.Application.Services;

public interface IVendorsService
{
    Task<Guid> CreateAsync(string name, CancellationToken ct = default);
    Task UpdateAsync(Guid id, string name, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
    Task<VendorDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<PagedResult<VendorDto>> GetAllAsync(string? searchTerm, int pageNumber = 1, int pageSize = 10, CancellationToken ct = default);
}
