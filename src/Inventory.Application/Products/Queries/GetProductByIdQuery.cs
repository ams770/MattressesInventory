using MediatR;
using Inventory.Domain.Interfaces;

namespace Inventory.Application.Products.Queries;

public record GetProductByIdQuery(Guid Id) : IRequest<ProductDto?>;

public class GetProductByIdQueryHandler(IProductRepository repository)
    : IRequestHandler<GetProductByIdQuery, ProductDto?>
{
    public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken ct)
    {
        var product = await repository.GetByIdAsync(request.Id, ct);
        if (product == null) return null;

        return new ProductDto(product.Id, product.Code, product.Name, product.Barcode, product.ImageUrl, product.Description, product.IsActive, product.CreatedAt);
    }
}
