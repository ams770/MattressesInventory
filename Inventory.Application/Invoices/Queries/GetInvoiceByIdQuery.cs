using MediatR;
using Inventory.Domain.Interfaces;

namespace Inventory.Application.Invoices.Queries;

public record GetInvoiceByIdQuery(Guid Id) : IRequest<InvoiceDto?>;

public class GetInvoiceByIdQueryHandler(IInvoiceRepository invoiceRepository)
    : IRequestHandler<GetInvoiceByIdQuery, InvoiceDto?>
{
    public async Task<InvoiceDto?> Handle(GetInvoiceByIdQuery request, CancellationToken ct)
    {
        var invoice = await invoiceRepository.GetByIdAsync(request.Id, ct);
        if (invoice == null) return null;

        return new InvoiceDto(
            invoice.Id,
            invoice.ClientId,
            invoice.TotalAmount,
            invoice.TotalDiscount,
            invoice.PaidAmount,
            invoice.TotalAmountDiscounted,
            invoice.TotalRemaining,
            invoice.InvoiceType,
            invoice.CreatedAt);
    }
}
