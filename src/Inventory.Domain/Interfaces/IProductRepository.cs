using Inventory.Domain.Entities;

namespace Inventory.Domain.Interfaces;

public interface IProductRepository : IRepository<Product, Guid>
{
}
