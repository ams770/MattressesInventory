namespace Inventory.Domain.Entities;

public abstract class Entity<TId>
{
    public TId Id { get; protected set; } = default!;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
}