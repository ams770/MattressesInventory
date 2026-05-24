using Microsoft.AspNetCore.Mvc;
using Inventory.Application.Products;
using Inventory.Application.Services;
using Inventory.Domain.Common;
using Inventory.API.Common;

namespace Inventory.API.Controllers;

public record CreateCategoryRequest(string Name, string? ImageUrl, string? Description, bool IsActive);
public record UpdateCategoryRequest(string Name, string? ImageUrl, string? Description, bool IsActive);

public class CategoriesController(ICategoriesService categoriesService) : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Result<Guid>>> Create([FromBody] CreateCategoryRequest request, CancellationToken ct)
    {
        var id = await categoriesService.CreateAsync(
            request.Name,
            request.ImageUrl,
            request.Description,
            request.IsActive,
            ct);
        return Ok(Result<Guid>.Success(id));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Result>> Update(Guid id, [FromBody] UpdateCategoryRequest request, CancellationToken ct)
    {
        await categoriesService.UpdateAsync(id, request.Name, request.ImageUrl, request.Description, request.IsActive, ct);
        return Ok(Result.Success());
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Result>> Delete(Guid id, CancellationToken ct)
    {
        await categoriesService.DeleteAsync(id, ct);
        return Ok(Result.Success());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Result<CategoryDto>>> GetById(Guid id, CancellationToken ct)
    {
        var result = await categoriesService.GetByIdAsync(id, ct);
        if (result == null) throw new KeyNotFoundException();
        return Ok(Result<CategoryDto>.Success(result));
    }

    [HttpGet]
    public async Task<ActionResult<Result<PagedResult<CategoryDto>>>> GetAll(
        [FromQuery] string? searchTerm,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken ct = default)
    {
        var result = await categoriesService.GetAllAsync(searchTerm, pageNumber, pageSize, ct);
        return Ok(Result<PagedResult<CategoryDto>>.Success(result));
    }
}
