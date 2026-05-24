using MediatR;
using Inventory.Domain.Interfaces;

namespace Inventory.Application.Invoices.Commands;

public record DeleteInvoiceCommand(Guid Id) : IRequest;

public class DeleteInvoiceCommandHandler(IInvoiceRepository invoiceRepository) : IRequestHandler<DeleteInvoiceCommand>
{
    public async Task Handle(DeleteInvoiceCommand request, CancellationToken ct)
    {
        var invoice = await invoiceRepository.GetByIdAsync(request.Id, ct);
        if (invoice == null) throw new KeyNotFoundException($"Invoice with ID {request.Id} was not found.");

        await invoiceRepository.DeleteAsync(invoice, ct);
    }
}
