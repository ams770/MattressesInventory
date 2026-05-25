using MediatR;
using Inventory.Domain.Interfaces;

namespace Inventory.Application.Purchases.Queries;

public record GetPurchaseByIdQuery(Guid Id) : IRequest<PurchaseDto?>;

public class GetPurchaseByIdQueryHandler(IPurchaseRepository repository)
    : IRequestHandler<GetPurchaseByIdQuery, PurchaseDto?>
{
    public async Task<PurchaseDto?> Handle(GetPurchaseByIdQuery request, CancellationToken ct)
    {
        var purchase = await repository.GetByIdAsync(request.Id, ct);
        if (purchase == null) return null;

        return new PurchaseDto(purchase.Id, purchase.VendorId, purchase.CreatedAt);
    }
}
