using MediatR;
using Inventory.Domain.Interfaces;

namespace Inventory.Application.Products.Queries;

public record GetCategoryByIdQuery(Guid Id) : IRequest<CategoryDto?>;

public class GetCategoryByIdQueryHandler(ICategoryRepository repository)
    : IRequestHandler<GetCategoryByIdQuery, CategoryDto?>
{
    public async Task<CategoryDto?> Handle(GetCategoryByIdQuery request, CancellationToken ct)
    {
        var category = await repository.GetByIdAsync(request.Id, ct);
        if (category == null) return null;

        return new CategoryDto(category.Id, category.Name, category.ImageUrl, category.Description, category.IsActive, category.CreatedAt);
    }
}
