using MediatR;
using Inventory.Domain.Enums;
using Inventory.Domain.Interfaces;

namespace Inventory.Application.Invoices.Commands;

public record UpdateInvoiceCommand(
    Guid Id, 
    Guid ClientId, 
    double TotalAmount, 
    double TotalDiscount, 
    double PaidAmount, 
    InvoiceType InvoiceType) : IRequest;

public class UpdateInvoiceCommandHandler(IInvoiceRepository invoiceRepository) : IRequestHandler<UpdateInvoiceCommand>
{
    public async Task Handle(UpdateInvoiceCommand request, CancellationToken ct)
    {
        var invoice = await invoiceRepository.GetByIdAsync(request.Id, ct);
        if (invoice == null) throw new KeyNotFoundException($"Invoice with ID {request.Id} was not found.");

        invoice.SetClientId(request.ClientId);
        invoice.SetTotals(request.TotalAmount, request.TotalDiscount, request.PaidAmount);
        invoice.SetInvoiceType(request.InvoiceType);

        await invoiceRepository.UpdateAsync(invoice, ct);
    }
}
