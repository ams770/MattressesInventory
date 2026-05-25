using Inventory.Application.Interfaces;
using MediatR;
using Inventory.Domain.Interfaces;

namespace Inventory.Application.Products.Commands;

public record UpdateCategoryCommand(Guid Id, string Name, string? ImageUrl, string? Description, bool IsActive) : IRequest;

public class UpdateCategoryCommandHandler(ICategoryRepository repository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateCategoryCommand>
{
    public async Task Handle(UpdateCategoryCommand request, CancellationToken ct)
    {
        var category = await repository.GetByIdAsync(request.Id, ct);
        if (category == null) throw new KeyNotFoundException($"Category with ID {request.Id} was not found.");

        category.SetName(request.Name);
        category.SetDescription(request.Description ?? "");
        category.SetImageUrl(request.ImageUrl ?? "");
        category.SetActive(request.IsActive);

        await repository.UpdateAsync(category, ct);
        await unitOfWork.SaveChangesAsync(ct);
    }
}
