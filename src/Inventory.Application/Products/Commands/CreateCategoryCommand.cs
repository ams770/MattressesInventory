using MediatR;
using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces;

namespace Inventory.Application.Products.Commands;

public record CreateCategoryCommand(string Name, string? ImageUrl, string? Description, bool IsActive) : IRequest<Guid>;

public class CreateCategoryCommandHandler(ICategoryRepository repository) : IRequestHandler<CreateCategoryCommand, Guid>
{
    public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken ct)
    {
        var category = Category.Create(request.Name, request.ImageUrl ?? "", request.Description ?? "", request.IsActive);
        await repository.AddAsync(category, ct);
        return category.Id;
    }
}
