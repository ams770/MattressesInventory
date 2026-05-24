using Microsoft.AspNetCore.Mvc;
using Inventory.Application.Products;
using Inventory.Application.Products.Commands;
using Inventory.Application.Products.Queries;
using Inventory.Domain.Common;
using Inventory.API.Common;

namespace Inventory.API.Controllers;

public record CreateProductRequest(
    string Code,
    string Name,
    string Barcode,
    bool IsActive,
    string? Description,
    string? ImageUrl);

public record UpdateProductRequest(
    string Code,
    string Name,
    string Barcode,
    bool IsActive,
    string? Description,
    string? ImageUrl);

public class ProductsController : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Result<Guid>>> Create([FromBody] CreateProductRequest request, CancellationToken ct)
    {
        var id = await Mediator.Send(new CreateProductCommand(
            request.Code,
            request.Name,
            request.Barcode,
            request.IsActive,
            request.Description,
            request.ImageUrl), ct);
        return Ok(Result<Guid>.Success(id));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Result>> Update(Guid id, [FromBody] UpdateProductRequest request, CancellationToken ct)
    {
        await Mediator.Send(new UpdateProductCommand(
            id,
            request.Code,
            request.Name,
            request.Barcode,
            request.IsActive,
            request.Description,
            request.ImageUrl), ct);
        return Ok(Result.Success());
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Result>> Delete(Guid id, CancellationToken ct)
    {
        await Mediator.Send(new DeleteProductCommand(id), ct);
        return Ok(Result.Success());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Result<ProductDto>>> GetById(Guid id, CancellationToken ct)
    {
        var result = await Mediator.Send(new GetProductByIdQuery(id), ct);
        if (result == null)
        {
            throw new KeyNotFoundException();
        }
        return Ok(Result<ProductDto>.Success(result));
    }

    [HttpGet]
    public async Task<ActionResult<Result<PagedResult<ProductDto>>>> GetAll(
        [FromQuery] string? searchTerm,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken ct = default)
    {
        var result = await Mediator.Send(new GetAllProductsQuery(searchTerm, pageNumber, pageSize), ct);
        return Ok(Result<PagedResult<ProductDto>>.Success(result));
    }
}
