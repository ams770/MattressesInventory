using Inventory.Domain.Enums;

namespace Inventory.Application.Invoices;

public record InvoiceDto(
    Guid Id,
    Guid ClientId,
    double TotalAmount,
    double TotalDiscount,
    double PaidAmount,
    double TotalAmountDiscounted,
    double TotalRemaining,
    InvoiceType InvoiceType,
    DateTime CreatedAt);
