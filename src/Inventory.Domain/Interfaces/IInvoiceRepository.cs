using Inventory.Domain.Entities;
using Inventory.Domain.Common;

namespace Inventory.Domain.Interfaces;

public interface IInvoiceRepository : IRepository<Invoice, Guid>
{
    Task<PagedResult<Invoice>> GetAllAsync(InvoicePagedRequest request, CancellationToken cancellationToken = default);
}
