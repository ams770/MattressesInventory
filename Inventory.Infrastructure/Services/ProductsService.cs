using MediatR;
using Inventory.Application.Products;
using Inventory.Application.Products.Commands;
using Inventory.Application.Products.Queries;
using Inventory.Application.Services;
using Inventory.Domain.Common;

namespace Inventory.Infrastructure.Services;

public class ProductsService(IMediator mediator) : IProductsService
{
    public Task<Guid> CreateAsync(string code, string name, string barcode, bool isActive, string? description, string? imageUrl, CancellationToken ct = default)
        => mediator.Send(new CreateProductCommand(code, name, barcode, isActive, description, imageUrl), ct);

    public Task UpdateAsync(Guid id, string code, string name, string barcode, bool isActive, string? description, string? imageUrl, CancellationToken ct = default)
        => mediator.Send(new UpdateProductCommand(id, code, name, barcode, isActive, description, imageUrl), ct);

    public Task DeleteAsync(Guid id, CancellationToken ct = default)
        => mediator.Send(new DeleteProductCommand(id), ct);

    public Task<ProductDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => mediator.Send(new GetProductByIdQuery(id), ct);

    public Task<PagedResult<ProductDto>> GetAllAsync(string? searchTerm, int pageNumber = 1, int pageSize = 10, CancellationToken ct = default)
        => mediator.Send(new GetAllProductsQuery(searchTerm, pageNumber, pageSize), ct);
}
