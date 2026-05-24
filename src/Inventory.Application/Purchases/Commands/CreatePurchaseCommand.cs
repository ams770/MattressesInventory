using MediatR;
using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces;

namespace Inventory.Application.Purchases.Commands;

public record CreatePurchaseCommand(Guid VendorId) : IRequest<Guid>;

public class CreatePurchaseCommandHandler(IPurchaseRepository repository) : IRequestHandler<CreatePurchaseCommand, Guid>
{
    public async Task<Guid> Handle(CreatePurchaseCommand request, CancellationToken ct)
    {
        var purchase = Purchase.Create(request.VendorId);
        await repository.AddAsync(purchase, ct);
        return purchase.Id;
    }
}
