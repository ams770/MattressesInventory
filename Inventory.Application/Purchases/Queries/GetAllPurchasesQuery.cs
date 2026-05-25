using MediatR;
using Inventory.Domain.Common;
using Inventory.Domain.Interfaces;

namespace Inventory.Application.Purchases.Queries;

public record GetAllPurchasesQuery(string? SearchTerm, int PageNumber = 1, int PageSize = 10)
    : IRequest<PagedResult<PurchaseDto>>;

public class GetAllPurchasesQueryHandler(IPurchaseRepository repository)
    : IRequestHandler<GetAllPurchasesQuery, PagedResult<PurchaseDto>>
{
    public async Task<PagedResult<PurchaseDto>> Handle(GetAllPurchasesQuery request, CancellationToken ct)
    {
        var pagedRequest = new PagedRequest
        {
            SearchTerm = request.SearchTerm,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };

        var result = await repository.GetAllAsync(pagedRequest, ct);
        var dtos = result.Items
            .Select(p => new PurchaseDto(p.Id, p.VendorId, p.CreatedAt))
            .ToList();

        return new PagedResult<PurchaseDto>(dtos, result.TotalCount, result.PageNumber, result.PageSize);
    }
}
