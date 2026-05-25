using MediatR;
using Inventory.Domain.Common;
using Inventory.Domain.Interfaces;

namespace Inventory.Application.Products.Queries;

public record GetAllCategoriesQuery(string? SearchTerm, int PageNumber = 1, int PageSize = 10) : IRequest<PagedResult<CategoryDto>>;

public class GetAllCategoriesQueryHandler(ICategoryRepository repository)
    : IRequestHandler<GetAllCategoriesQuery, PagedResult<CategoryDto>>
{
    public async Task<PagedResult<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken ct)
    {
        var pagedRequest = new PagedRequest
        {
            SearchTerm = request.SearchTerm,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };

        var result = await repository.GetAllAsync(pagedRequest, ct);
        var dtos = result.Items.Select(x => new CategoryDto(x.Id, x.Name, x.ImageUrl, x.Description, x.IsActive, x.CreatedAt)).ToList();

        return new PagedResult<CategoryDto>(dtos, result.TotalCount, result.PageNumber, result.PageSize);
    }
}
