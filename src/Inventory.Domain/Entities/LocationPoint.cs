using Inventory.Domain.Exceptions;

namespace Inventory.Domain.Entities;

public class LocationPoint : Entity<Guid>
{
    public string Name { get; private set; }

    private LocationPoint()
    {
    }

    public static LocationPoint Create(string name)
    {
        ValidateName(name);

        return new LocationPoint
        {
            Id = Guid.NewGuid(),
            Name = name
        };
    }

    // Setters
    public void SetName(string name)
    {
        ValidateName(name);
        Name = name;
    }

    // Validators
    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > 100)
            throw new LocationPointExceptions.InValidNameException();
    }
}