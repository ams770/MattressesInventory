using Microsoft.AspNetCore.Mvc;
using Inventory.Application.LocationPoints;
using Inventory.Application.Services;
using Inventory.Domain.Common;
using Inventory.API.Common;

namespace Inventory.API.Controllers;

public record CreateLocationPointRequest(string Name);
public record UpdateLocationPointRequest(string Name);

public class LocationPointsController(ILocationPointsService locationPointsService) : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Result<Guid>>> Create([FromBody] CreateLocationPointRequest request, CancellationToken ct)
    {
        var id = await locationPointsService.CreateAsync(request.Name, ct);
        return Ok(Result<Guid>.Success(id));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Result>> Update(Guid id, [FromBody] UpdateLocationPointRequest request, CancellationToken ct)
    {
        await locationPointsService.UpdateAsync(id, request.Name, ct);
        return Ok(Result.Success());
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Result>> Delete(Guid id, CancellationToken ct)
    {
        await locationPointsService.DeleteAsync(id, ct);
        return Ok(Result.Success());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Result<LocationPointDto>>> GetById(Guid id, CancellationToken ct)
    {
        var result = await locationPointsService.GetByIdAsync(id, ct);
        if (result == null) throw new KeyNotFoundException();
        return Ok(Result<LocationPointDto>.Success(result));
    }

    [HttpGet]
    public async Task<ActionResult<Result<PagedResult<LocationPointDto>>>> GetAll(
        [FromQuery] string? searchTerm,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken ct = default)
    {
        var result = await locationPointsService.GetAllAsync(searchTerm, pageNumber, pageSize, ct);
        return Ok(Result<PagedResult<LocationPointDto>>.Success(result));
    }
}
