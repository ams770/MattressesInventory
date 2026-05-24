using Inventory.Application.LocationPoints;
using Inventory.Domain.Common;

namespace Inventory.Application.Services;

public interface ILocationPointsService
{
    Task<Guid> CreateAsync(string name, CancellationToken ct = default);
    Task UpdateAsync(Guid id, string name, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
    Task<LocationPointDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<PagedResult<LocationPointDto>> GetAllAsync(string? searchTerm, int pageNumber = 1, int pageSize = 10, CancellationToken ct = default);
}