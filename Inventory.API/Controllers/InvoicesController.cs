using Microsoft.AspNetCore.Mvc;
using Inventory.Application.Invoices;
using Inventory.Application.Services;
using Inventory.Domain.Common;
using Inventory.Domain.Enums;
using Inventory.API.Common;

namespace Inventory.API.Controllers;

public record CreateInvoiceProductRequest(
    Guid Id,
    double SoldByPrice,
    int Qty);

public record CreateInvoiceRequest(
    Guid ClientId,
    double PaidAmount,
    InvoiceType InvoiceType,
    List<CreateInvoiceProductRequest> Products);

public record UpdateInvoiceRequest(
    Guid ClientId,
    double TotalAmount,
    double TotalDiscount,
    double PaidAmount,
    InvoiceType InvoiceType);

public class InvoicesController(IInvoicesService invoicesService) : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Result<Guid>>> Create([FromBody] CreateInvoiceRequest request, CancellationToken ct)
    {
        var id = await invoicesService.CreateAsync(
            request.ClientId,
            request.PaidAmount,
            request.InvoiceType,
            request.Products.Select(p => new CreateInvoiceProductDto(p.Id, p.SoldByPrice, p.Qty)).ToList(),
            ct);
        return Ok(Result<Guid>.Success(id));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Result>> Update(Guid id, [FromBody] UpdateInvoiceRequest request, CancellationToken ct)
    {
        await invoicesService.UpdateAsync(
            id,
            request.ClientId,
            request.TotalAmount,
            request.TotalDiscount,
            request.PaidAmount,
            request.InvoiceType,
            ct);
        return Ok(Result.Success());
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Result>> Delete(Guid id, CancellationToken ct)
    {
        await invoicesService.DeleteAsync(id, ct);
        return Ok(Result.Success());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Result<InvoiceDto>>> GetById(Guid id, CancellationToken ct)
    {
        var result = await invoicesService.GetByIdAsync(id, ct);
        if (result == null) throw new KeyNotFoundException();
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
        var result = await invoicesService.GetAllAsync(searchTerm, pageNumber, pageSize, clientId, fromDate, toDate, ct);
        return Ok(Result<PagedResult<InvoiceDto>>.Success(result));
    }
}
