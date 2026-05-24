using MediatR;
using Inventory.Domain.Common;
using Inventory.Domain.Interfaces;

namespace Inventory.Application.Products.Queries;

public record GetAllProductsQuery(PagedRequest Request) : IRequest<PagedResult<ProductDto>>;

public class GetAllProductsQueryHandler(IProductRepository repository)
    : IRequestHandler<GetAllProductsQuery, PagedResult<ProductDto>>
{
    public async Task<PagedResult<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken ct)
    {
        var result = await repository.GetAllAsync(request.Request, ct);
        var dtos = result.Items.Select(x => new ProductDto(x.Id, x.Code, x.Name, x.Barcode, x.ImageUrl, x.Description, x.IsActive, x.CreatedAt)).ToList();

        return new PagedResult<ProductDto>(dtos, result.TotalCount, result.PageNumber, result.PageSize);
    }
}
