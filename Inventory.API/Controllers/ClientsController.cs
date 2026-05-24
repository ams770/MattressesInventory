using Microsoft.AspNetCore.Mvc;
using Inventory.Application.Clients;
using Inventory.Application.Clients.Commands;
using Inventory.Application.Clients.Queries;
using Inventory.Domain.Common;
using Maintrols.Shared.SharedKernel.Primitives;

namespace Inventory.API.Controllers;

public record CreateClientRequest(string Name, string PhoneNumber);
public record UpdateClientRequest(string Name, string PhoneNumber);

public class ClientsController : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Result<Guid>>> Create([FromBody] CreateClientRequest request, CancellationToken ct)
    {
        var id = await Mediator.Send(new CreateClientCommand(request.Name, request.PhoneNumber), ct);
        return Ok(Result<Guid>.Success(id));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Result>> Update(Guid id, [FromBody] UpdateClientRequest request, CancellationToken ct)
    {
        await Mediator.Send(new UpdateClientCommand(id, request.Name, request.PhoneNumber), ct);
        return Ok(Result.Success());
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Result>> Delete(Guid id, CancellationToken ct)
    {
        await Mediator.Send(new DeleteClientCommand(id), ct);
        return Ok(Result.Success());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Result<ClientDto>>> GetById(Guid id, CancellationToken ct)
    {
        var client = await Mediator.Send(new GetClientByIdQuery(id), ct);
        if (client == null)
        {
            throw new KeyNotFoundException();
        }
        return Ok(Result<ClientDto>.Success(client));
    }

    [HttpGet]
    public async Task<ActionResult<Result<PagedResult<ClientDto>>>> GetAll(
        [FromQuery] string? searchTerm,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken ct = default)
    {
        var result = await Mediator.Send(new GetAllClientsQuery(searchTerm, pageNumber, pageSize), ct);
        return Ok(Result<PagedResult<ClientDto>>.Success(result));
    }
}
