using Inventory.Application.Interfaces;
using MediatR;
using Inventory.Domain.Interfaces;

namespace Inventory.Application.Products.Commands;

public record UpdateProductCommand(Guid Id, string Code, string Name, string Barcode, bool IsActive, string? Description, string? ImageUrl) : IRequest;

public class UpdateProductCommandHandler(IProductRepository repository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateProductCommand>
{
    public async Task Handle(UpdateProductCommand request, CancellationToken ct)
    {
        var product = await repository.GetByIdAsync(request.Id, ct);
        if (product == null) throw new KeyNotFoundException($"Product with ID {request.Id} was not found.");

        product.SetCode(request.Code);
        product.SetName(request.Name);
        product.SetBarcode(request.Barcode);
        product.SetActive(request.IsActive);
        product.SetDescription(request.Description ?? "");
        product.SetImageUrl(request.ImageUrl ?? "");

        await repository.UpdateAsync(product, ct);
        await unitOfWork.SaveChangesAsync(ct);
    }
}
