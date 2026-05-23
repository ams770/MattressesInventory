using Inventory.Domain.Entities;

namespace Inventory.Domain.Interfaces;

public interface ICategoryProductRepository : IRepository<CategoryProduct, Guid>
{
}
