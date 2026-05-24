using MediatR;
using Inventory.Domain.Common;
using Inventory.Domain.Interfaces;

namespace Inventory.Application.Products.Queries;

public record GetAllProductsQuery(string? SearchTerm, int PageNumber = 1, int PageSize = 10) : IRequest<PagedResult<ProductDto>>;

public class GetAllProductsQueryHandler(IProductRepository repository)
    : IRequestHandler<GetAllProductsQuery, PagedResult<ProductDto>>
{
    public async Task<PagedResult<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken ct)
    {
        var pagedRequest = new PagedRequest
        {
            SearchTerm = request.SearchTerm,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };

        var result = await repository.GetAllAsync(pagedRequest, ct);
        var dtos = result.Items.Select(x => new ProductDto(x.Id, x.Code, x.Name, x.Barcode, x.ImageUrl, x.Description, x.IsActive, x.CreatedAt)).ToList();

        return new PagedResult<ProductDto>(dtos, result.TotalCount, result.PageNumber, result.PageSize);
    }
}
