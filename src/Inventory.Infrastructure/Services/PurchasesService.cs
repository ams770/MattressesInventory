using MediatR;
using Inventory.Application.Purchases;
using Inventory.Application.Purchases.Commands;
using Inventory.Application.Purchases.Queries;
using Inventory.Application.Services;
using Inventory.Domain.Common;

namespace Inventory.Infrastructure.Services;

public class PurchasesService(IMediator mediator) : IPurchasesService
{
    public Task<Guid> CreateAsync(Guid vendorId, CancellationToken ct = default)
        => mediator.Send(new CreatePurchaseCommand(vendorId), ct);

    public Task UpdateAsync(Guid id, Guid vendorId, CancellationToken ct = default)
        => mediator.Send(new UpdatePurchaseCommand(id, vendorId), ct);

    public Task<PurchaseDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => mediator.Send(new GetPurchaseByIdQuery(id), ct);

    public Task<PagedResult<PurchaseDto>> GetAllAsync(string? searchTerm, int pageNumber = 1, int pageSize = 10, CancellationToken ct = default)
        => mediator.Send(new GetAllPurchasesQuery(searchTerm, pageNumber, pageSize), ct);
}
