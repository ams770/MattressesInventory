using MediatR;
using Inventory.Application.LocationPoints;
using Inventory.Application.LocationPoints.Commands;
using Inventory.Application.LocationPoints.Queries;
using Inventory.Application.Services;
using Inventory.Domain.Common;

namespace Inventory.Infrastructure.Services;

public class LocationPointsService(IMediator mediator) : ILocationPointsService
{
    public Task<Guid> CreateAsync(string name, CancellationToken ct = default)
        => mediator.Send(new CreateLocationPointCommand(name), ct);

    public Task UpdateAsync(Guid id, string name, CancellationToken ct = default)
        => mediator.Send(new UpdateLocationPointCommand(id, name), ct);

    public Task DeleteAsync(Guid id, CancellationToken ct = default)
        => mediator.Send(new DeleteLocationPointCommand(id), ct);

    public Task<LocationPointDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => mediator.Send(new GetLocationPointByIdQuery(id), ct);

    public Task<PagedResult<LocationPointDto>> GetAllAsync(string? searchTerm, int pageNumber = 1, int pageSize = 10, CancellationToken ct = default)
        => mediator.Send(new GetAllLocationPointsQuery(searchTerm, pageNumber, pageSize), ct);
}
