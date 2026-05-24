using Inventory.Application.Interfaces;
using MediatR;
using Inventory.Domain.Entities;
using Inventory.Domain.Enums;
using Inventory.Domain.Interfaces;

namespace Inventory.Application.Invoices.Commands;

public record CreateInvoiceCommand(
    Guid ClientId,
    double TotalAmount,
    double TotalDiscount,
    double PaidAmount,
    InvoiceType InvoiceType) : IRequest<Guid>;

public class CreateInvoiceCommandHandler(IInvoiceRepository invoiceRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<CreateInvoiceCommand, Guid>
{
    public async Task<Guid> Handle(CreateInvoiceCommand request, CancellationToken ct)
    {
        var invoice = Invoice.Create(
            request.ClientId,
            request.TotalAmount,
            request.TotalDiscount,
            request.PaidAmount,
            request.InvoiceType);

        await invoiceRepository.AddAsync(invoice, ct);
        await unitOfWork.SaveChangesAsync(ct);
        return invoice.Id;
    }
}
