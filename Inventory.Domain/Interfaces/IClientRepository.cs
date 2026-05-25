using Inventory.Domain.Entities;

namespace Inventory.Domain.Interfaces;

public interface IClientRepository : IRepository<Client, Guid>
{
}
