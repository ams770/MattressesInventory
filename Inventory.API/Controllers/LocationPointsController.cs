using Microsoft.AspNetCore.Mvc;
using Inventory.Application.LocationPoints;
using Inventory.Application.LocationPoints.Commands;
using Inventory.Application.LocationPoints.Queries;
using Inventory.Domain.Common;
using Maintrols.Shared.SharedKernel.Primitives;

namespace Inventory.API.Controllers;

public record CreateLocationPointRequest(string Name);
public record UpdateLocationPointRequest(string Name);

public class LocationPointsController : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Result<Guid>>> Create([FromBody] CreateLocationPointRequest request, CancellationToken ct)
    {
        var id = await Mediator.Send(new CreateLocationPointCommand(request.Name), ct);
        return Ok(Result<Guid>.Success(id));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Result>> Update(Guid id, [FromBody] UpdateLocationPointRequest request, CancellationToken ct)
    {
        await Mediator.Send(new UpdateLocationPointCommand(id, request.Name), ct);
        return Ok(Result.Success());
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Result>> Delete(Guid id, CancellationToken ct)
    {
        await Mediator.Send(new DeleteLocationPointCommand(id), ct);
        return Ok(Result.Success());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Result<LocationPointDto>>> GetById(Guid id, CancellationToken ct)
    {
        var result = await Mediator.Send(new GetLocationPointByIdQuery(id), ct);
        if (result == null)
        {
            throw new KeyNotFoundException();
        }
        return Ok(Result<LocationPointDto>.Success(result));
    }

    [HttpGet]
    public async Task<ActionResult<Result<PagedResult<LocationPointDto>>>> GetAll(
        [FromQuery] string? searchTerm,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken ct = default)
    {
        var result = await Mediator.Send(new GetAllLocationPointsQuery(searchTerm, pageNumber, pageSize), ct);
        return Ok(Result<PagedResult<LocationPointDto>>.Success(result));
    }
}
