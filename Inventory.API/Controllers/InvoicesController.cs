using Microsoft.AspNetCore.Mvc;
using Inventory.Application.Invoices;
using Inventory.Application.Invoices.Commands;
using Inventory.Application.Invoices.Queries;
using Inventory.Domain.Common;
using Inventory.Domain.Enums;
using Maintrols.Shared.SharedKernel.Primitives;

namespace Inventory.API.Controllers;

public record CreateInvoiceRequest(
    Guid ClientId,
    double TotalAmount,
    double TotalDiscount,
    double PaidAmount,
    InvoiceType InvoiceType);

public record UpdateInvoiceRequest(
    Guid ClientId,
    double TotalAmount,
    double TotalDiscount,
    double PaidAmount,
    InvoiceType InvoiceType);

public class InvoicesController : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Result<Guid>>> Create([FromBody] CreateInvoiceRequest request, CancellationToken ct)
    {
        var id = await Mediator.Send(new CreateInvoiceCommand(
            request.ClientId,
            request.TotalAmount,
            request.TotalDiscount,
            request.PaidAmount,
            request.InvoiceType), ct);
        return Ok(Result<Guid>.Success(id));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Result>> Update(Guid id, [FromBody] UpdateInvoiceRequest request, CancellationToken ct)
    {
        await Mediator.Send(new UpdateInvoiceCommand(
            id,
            request.ClientId,
            request.TotalAmount,
            request.TotalDiscount,
            request.PaidAmount,
            request.InvoiceType), ct);
        return Ok(Result.Success());
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Result>> Delete(Guid id, CancellationToken ct)
    {
        await Mediator.Send(new DeleteInvoiceCommand(id), ct);
        return Ok(Result.Success());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Result<InvoiceDto>>> GetById(Guid id, CancellationToken ct)
    {
        var result = await Mediator.Send(new GetInvoiceByIdQuery(id), ct);
        if (result == null)
        {
            throw new KeyNotFoundException();
        }
        return Ok(Result<InvoiceDto>.Success(result));
    }

    [HttpGet]
    public async Task<ActionResult<Result<PagedResult<InvoiceDto>>>> GetAll(
        [FromQuery] string? searchTerm,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] Guid? clientId = null,
        [FromQuery] DateTime? fromDate = null,
        [FromQuery] DateTime? toDate = null,
        CancellationToken ct = default)
    {
        var result = await Mediator.Send(new GetAllInvoicesQuery(
            searchTerm,
            pageNumber,
            pageSize,
            clientId,
            fromDate,
            toDate), ct);
        return Ok(Result<PagedResult<InvoiceDto>>.Success(result));
    }
}
