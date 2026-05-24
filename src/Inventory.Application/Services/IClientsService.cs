using Inventory.Application.Clients;
using Inventory.Domain.Common;

namespace Inventory.Application.Services;

public interface IClientsService
{
    Task<Guid> CreateAsync(string name, string phoneNumber, CancellationToken ct = default);
    Task UpdateAsync(Guid id, string name, string phoneNumber, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
    Task<ClientDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<PagedResult<ClientDto>> GetAllAsync(string? searchTerm, int pageNumber = 1, int pageSize = 10, CancellationToken ct = default);
}
