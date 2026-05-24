using MediatR;
using Inventory.Domain.Interfaces;

namespace Inventory.Application.Products.Commands;

public record DeleteProductCommand(Guid Id) : IRequest;

public class DeleteProductCommandHandler(IProductRepository repository) : IRequestHandler<DeleteProductCommand>
{
    public async Task Handle(DeleteProductCommand request, CancellationToken ct)
    {
        var product = await repository.GetByIdAsync(request.Id, ct);
        if (product == null) throw new KeyNotFoundException($"Product with ID {request.Id} was not found.");

        await repository.DeleteAsync(product, ct);
    }
}
