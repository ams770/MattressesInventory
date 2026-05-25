using Inventory.Application.Interfaces;
using MediatR;
using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces;

namespace Inventory.Application.Products.Commands;

public record CreateProductCommand(string Code, string Name, string Barcode, bool IsActive, string? Description, string? ImageUrl) : IRequest<Guid>;

public class CreateProductCommandHandler(IProductRepository repository, IUnitOfWork unitOfWork) : IRequestHandler<CreateProductCommand, Guid>
{
    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken ct)
    {
        var product = Product.Create(request.Code, request.Name, request.Barcode, request.IsActive, request.Description, request.ImageUrl);
        await repository.AddAsync(product, ct);
        await unitOfWork.SaveChangesAsync(ct);
        return product.Id;
    }
}
