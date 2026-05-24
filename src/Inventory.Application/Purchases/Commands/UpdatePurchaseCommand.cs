using MediatR;
using Inventory.Domain.Interfaces;

namespace Inventory.Application.Purchases.Commands;

public record UpdatePurchaseCommand(Guid Id, Guid VendorId) : IRequest;

public class UpdatePurchaseCommandHandler(IPurchaseRepository repository) : IRequestHandler<UpdatePurchaseCommand>
{
    public async Task Handle(UpdatePurchaseCommand request, CancellationToken ct)
    {
        var purchase = await repository.GetByIdAsync(request.Id, ct);
        if (purchase == null) throw new KeyNotFoundException($"Purchase with ID {request.Id} was not found.");

        purchase.SetVendorId(request.VendorId);
        await repository.UpdateAsync(purchase, ct);
    }
}
