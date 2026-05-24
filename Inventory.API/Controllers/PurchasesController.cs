using Microsoft.AspNetCore.Mvc;
using Inventory.Application.Purchases.Commands;
using Inventory.API.Common;

namespace Inventory.API.Controllers;

public record CreatePurchaseRequest(Guid VendorId);
public record UpdatePurchaseRequest(Guid VendorId);

public class PurchasesController : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Result<Guid>>> Create([FromBody] CreatePurchaseRequest request, CancellationToken ct)
    {
        var id = await Mediator.Send(new CreatePurchaseCommand(request.VendorId), ct);
        return Ok(Result<Guid>.Success(id));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Result>> Update(Guid id, [FromBody] UpdatePurchaseRequest request, CancellationToken ct)
    {
        await Mediator.Send(new UpdatePurchaseCommand(id, request.VendorId), ct);
        return Ok(Result.Success());
    }
}
