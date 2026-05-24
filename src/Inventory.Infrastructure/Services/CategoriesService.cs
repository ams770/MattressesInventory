using MediatR;
using Inventory.Application.Products;
using Inventory.Application.Products.Commands;
using Inventory.Application.Products.Queries;
using Inventory.Application.Services;
using Inventory.Domain.Common;

namespace Inventory.Infrastructure.Services;

public class CategoriesService(IMediator mediator) : ICategoriesService
{
    public Task<Guid> CreateAsync(string name, string? imageUrl, string? description, bool isActive, CancellationToken ct = default)
        => mediator.Send(new CreateCategoryCommand(name, imageUrl, description, isActive), ct);

    public Task UpdateAsync(Guid id, string name, string? imageUrl, string? description, bool isActive, CancellationToken ct = default)
        => mediator.Send(new UpdateCategoryCommand(id, name, imageUrl, description, isActive), ct);

    public Task DeleteAsync(Guid id, CancellationToken ct = default)
        => mediator.Send(new DeleteCategoryCommand(id), ct);

    public Task<CategoryDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => mediator.Send(new GetCategoryByIdQuery(id), ct);

    public Task<PagedResult<CategoryDto>> GetAllAsync(string? searchTerm, int pageNumber = 1, int pageSize = 10, CancellationToken ct = default)
        => mediator.Send(new GetAllCategoriesQuery(searchTerm, pageNumber, pageSize), ct);
}
