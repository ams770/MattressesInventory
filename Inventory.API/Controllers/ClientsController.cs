using Microsoft.AspNetCore.Mvc;
using Inventory.Application.Clients;
using Inventory.Application.Services;
using Inventory.Domain.Common;
using Inventory.API.Common;

namespace Inventory.API.Controllers;

public record CreateClientRequest(string Name, string PhoneNumber);
public record UpdateClientRequest(string Name, string PhoneNumber);

public class ClientsController(IClientsService clientsService) : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Result<Guid>>> Create([FromBody] CreateClientRequest request, CancellationToken ct)
    {
        var id = await clientsService.CreateAsync(request.Name, request.PhoneNumber, ct);
        return Ok(Result<Guid>.Success(id));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Result>> Update(Guid id, [FromBody] UpdateClientRequest request, CancellationToken ct)
    {
        await clientsService.UpdateAsync(id, request.Name, request.PhoneNumber, ct);
        return Ok(Result.Success());
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Result>> Delete(Guid id, CancellationToken ct)
    {
        await clientsService.DeleteAsync(id, ct);
        return Ok(Result.Success());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Result<ClientDto>>> GetById(Guid id, CancellationToken ct)
    {
        var client = await clientsService.GetByIdAsync(id, ct);
        if (client == null) throw new KeyNotFoundException();
        return Ok(Result<ClientDto>.Success(client));
    }

    [HttpGet]
    public async Task<ActionResult<Result<PagedResult<ClientDto>>>> GetAll(
        [FromQuery] string? searchTerm,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken ct = default)
    {
        var result = await clientsService.GetAllAsync(searchTerm, pageNumber, pageSize, ct);
        return Ok(Result<PagedResult<ClientDto>>.Success(result));
    }
}
