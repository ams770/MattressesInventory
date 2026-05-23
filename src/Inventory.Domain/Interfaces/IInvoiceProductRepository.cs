using Inventory.Domain.Entities;

namespace Inventory.Domain.Interfaces;

public interface IInvoiceProductRepository : IRepository<InvoiceProduct, Guid>
{
}
