using Inventory.Application.Purchases;
using Inventory.Domain.Common;

namespace Inventory.Application.Services;

public interface IPurchasesService
{
    Task<Guid> CreateAsync(Guid vendorId, CancellationToken ct = default);
    Task UpdateAsync(Guid id, Guid vendorId, CancellationToken ct = default);
    Task<PurchaseDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<PagedResult<PurchaseDto>> GetAllAsync(string? searchTerm, int pageNumber = 1, int pageSize = 10, CancellationToken ct = default);
}