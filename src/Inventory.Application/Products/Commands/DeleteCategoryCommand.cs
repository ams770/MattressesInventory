using Inventory.Application.Interfaces;
using MediatR;
using Inventory.Domain.Interfaces;

namespace Inventory.Application.Products.Commands;

public record DeleteCategoryCommand(Guid Id) : IRequest;

public class DeleteCategoryCommandHandler(ICategoryRepository repository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteCategoryCommand>
{
    public async Task Handle(DeleteCategoryCommand request, CancellationToken ct)
    {
        var category = await repository.GetByIdAsync(request.Id, ct);
        if (category == null) throw new KeyNotFoundException($"Category with ID {request.Id} was not found.");

        await repository.DeleteAsync(category, ct);
        await unitOfWork.SaveChangesAsync(ct);
    }
}
