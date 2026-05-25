using MediatR;
using Inventory.Application.Vendors;
using Inventory.Application.Vendors.Commands;
using Inventory.Application.Vendors.Queries;
using Inventory.Application.Services;
using Inventory.Domain.Common;

namespace Inventory.Infrastructure.Services;

public class VendorsService(IMediator mediator) : IVendorsService
{
    public Task<Guid> CreateAsync(string name, CancellationToken ct = default)
        => mediator.Send(new CreateVendorCommand(name), ct);

    public Task UpdateAsync(Guid id, string name, CancellationToken ct = default)
        => mediator.Send(new UpdateVendorCommand(id, name), ct);

    public Task DeleteAsync(Guid id, CancellationToken ct = default)
        => mediator.Send(new DeleteVendorCommand(id), ct);

    public Task<VendorDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => mediator.Send(new GetVendorByIdQuery(id), ct);

    public Task<PagedResult<VendorDto>> GetAllAsync(string? searchTerm, int pageNumber = 1, int pageSize = 10, CancellationToken ct = default)
        => mediator.Send(new GetAllVendorsQuery(searchTerm, pageNumber, pageSize), ct);
}
