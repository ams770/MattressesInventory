using Inventory.Application.Invoices;
using Inventory.Domain.Common;
using Inventory.Domain.Enums;

namespace Inventory.Application.Services;

public interface IInvoicesService
{
    Task<Guid> CreateAsync(Guid clientId, double totalAmount, double totalDiscount, double paidAmount, InvoiceType invoiceType, CancellationToken ct = default);
    Task UpdateAsync(Guid id, Guid clientId, double totalAmount, double totalDiscount, double paidAmount, InvoiceType invoiceType, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
    Task<InvoiceDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<PagedResult<InvoiceDto>> GetAllAsync(string? searchTerm, int pageNumber = 1, int pageSize = 10, Guid? clientId = null, DateTime? fromDate = null, DateTime? toDate = null, CancellationToken ct = default);
}