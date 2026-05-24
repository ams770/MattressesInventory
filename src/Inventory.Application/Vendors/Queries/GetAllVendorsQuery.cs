using MediatR;
using Inventory.Domain.Common;
using Inventory.Domain.Interfaces;

namespace Inventory.Application.Vendors.Queries;

public record GetAllVendorsQuery(string? SearchTerm, int PageNumber = 1, int PageSize = 10)
    : IRequest<PagedResult<VendorDto>>;

public class GetAllVendorsQueryHandler(IVendorRepository repository)
    : IRequestHandler<GetAllVendorsQuery, PagedResult<VendorDto>>
{
    public async Task<PagedResult<VendorDto>> Handle(GetAllVendorsQuery request, CancellationToken ct)
    {
        var pagedRequest = new PagedRequest
        {
            SearchTerm = request.SearchTerm,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };

        var result = await repository.GetAllAsync(pagedRequest, ct);
        var dtos = result.Items
            .Select(v => new VendorDto(v.Id, v.Name, v.CreatedAt))
            .ToList();

        return new PagedResult<VendorDto>(dtos, result.TotalCount, result.PageNumber, result.PageSize);
    }
}
