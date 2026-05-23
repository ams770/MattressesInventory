using Inventory.Domain.Entities;

namespace Inventory.Domain.Interfaces;

public interface IPurchaseRepository : IRepository<Purchase, Guid>
{
}
