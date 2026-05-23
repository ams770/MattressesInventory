using Inventory.Domain.Exceptions;

namespace Inventory.Domain.Entities;

public class Vendor : Entity<Guid>
{
    public string Name { get; private set; }

    private Vendor()
    {
    }

    public static Vendor Create(string name)
    {
        ValidateName(name);

        return new Vendor
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
            throw new VendorExceptions.InValidNameException();
    }
}