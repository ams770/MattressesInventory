using MediatR;
using Inventory.Application.Invoices;
using Inventory.Application.Invoices.Commands;
using Inventory.Application.Invoices.Queries;
using Inventory.Application.Services;
using Inventory.Domain.Common;
using Inventory.Domain.Enums;

namespace Inventory.Infrastructure.Services;

public class InvoicesService(IMediator mediator) : IInvoicesService
{
    public Task<Guid> CreateAsync(Guid clientId, double paidAmount, InvoiceType invoiceType, List<CreateInvoiceProductDto> products, CancellationToken ct = default)
        => mediator.Send(new CreateInvoiceCommand(
            clientId,
            paidAmount,
            invoiceType,
            products.Select(p => new CreateInvoiceProductCommand(p.Id, p.SoldByPrice, p.Qty)).ToList()), ct);

    public Task UpdateAsync(Guid id, Guid clientId, double totalAmount, double totalDiscount, double paidAmount, InvoiceType invoiceType, CancellationToken ct = default)
        => mediator.Send(new UpdateInvoiceCommand(id, clientId, totalAmount, totalDiscount, paidAmount, invoiceType), ct);

    public Task DeleteAsync(Guid id, CancellationToken ct = default)
        => mediator.Send(new DeleteInvoiceCommand(id), ct);

    public Task<InvoiceDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => mediator.Send(new GetInvoiceByIdQuery(id), ct);

    public Task<PagedResult<InvoiceDto>> GetAllAsync(string? searchTerm, int pageNumber = 1, int pageSize = 10, Guid? clientId = null, DateTime? fromDate = null, DateTime? toDate = null, CancellationToken ct = default)
        => mediator.Send(new GetAllInvoicesQuery(searchTerm, pageNumber, pageSize, clientId, fromDate, toDate), ct);
}
