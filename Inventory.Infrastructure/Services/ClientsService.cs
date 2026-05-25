using MediatR;
using Inventory.Application.Clients;
using Inventory.Application.Clients.Commands;
using Inventory.Application.Clients.Queries;
using Inventory.Application.Services;
using Inventory.Domain.Common;

namespace Inventory.Infrastructure.Services;

public class ClientsService(IMediator mediator) : IClientsService
{
    public Task<Guid> CreateAsync(string name, string phoneNumber, CancellationToken ct = default)
        => mediator.Send(new CreateClientCommand(name, phoneNumber), ct);

    public Task UpdateAsync(Guid id, string name, string phoneNumber, CancellationToken ct = default)
        => mediator.Send(new UpdateClientCommand(id, name, phoneNumber), ct);

    public Task DeleteAsync(Guid id, CancellationToken ct = default)
        => mediator.Send(new DeleteClientCommand(id), ct);

    public Task<ClientDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => mediator.Send(new GetClientByIdQuery(id), ct);

    public Task<PagedResult<ClientDto>> GetAllAsync(string? searchTerm, int pageNumber = 1, int pageSize = 10, CancellationToken ct = default)
        => mediator.Send(new GetAllClientsQuery(searchTerm, pageNumber, pageSize), ct);
}
