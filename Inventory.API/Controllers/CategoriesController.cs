using Microsoft.AspNetCore.Mvc;
using Inventory.Application.Products;
using Inventory.Application.Products.Commands;
using Inventory.Application.Products.Queries;
using Inventory.Domain.Common;
using Inventory.API.Common;

namespace Inventory.API.Controllers;

public record CreateCategoryRequest(string Name, string? ImageUrl, string? Description, bool IsActive);
public record UpdateCategoryRequest(string Name, string? ImageUrl, string? Description, bool IsActive);

public class CategoriesController : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Result<Guid>>> Create([FromBody] CreateCategoryRequest request, CancellationToken ct)
    {
        var id = await Mediator.Send(new CreateCategoryCommand(
            request.Name,
            request.ImageUrl,
            request.Description,
            request.IsActive), ct);
        return Ok(Result<Guid>.Success(id));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Result>> Update(Guid id, [FromBody] UpdateCategoryRequest request, CancellationToken ct)
    {
        await Mediator.Send(new UpdateCategoryCommand(
            id,
            request.Name,
            request.ImageUrl,
            request.Description,
            request.IsActive), ct);
        return Ok(Result.Success());
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Result>> Delete(Guid id, CancellationToken ct)
    {
        await Mediator.Send(new DeleteCategoryCommand(id), ct);
        return Ok(Result.Success());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Result<CategoryDto>>> GetById(Guid id, CancellationToken ct)
    {
        var result = await Mediator.Send(new GetCategoryByIdQuery(id), ct);
        if (result == null)
        {
            throw new KeyNotFoundException();
        }
        return Ok(Result<CategoryDto>.Success(result));
    }

    [HttpGet]
    public async Task<ActionResult<Result<PagedResult<CategoryDto>>>> GetAll(
        [FromQuery] string? searchTerm,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken ct = default)
    {
        var result = await Mediator.Send(new GetAllCategoriesQuery(searchTerm, pageNumber, pageSize), ct);
        return Ok(Result<PagedResult<CategoryDto>>.Success(result));
    }
}
