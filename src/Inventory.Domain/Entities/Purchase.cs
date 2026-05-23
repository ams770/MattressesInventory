using Inventory.Domain.Exceptions;

namespace Inventory.Domain.Entities;

public class Purchase : Entity<Guid>
{
    public Guid VendorId { get; private set; }
    public Vendor Vendor { get; private set; } = null!;

    private Purchase()
    {
    }

    public static Purchase Create(Guid vendorId)
    {
        ValidateVendorId(vendorId);

        return new Purchase
        {
            Id = Guid.NewGuid(),
            VendorId = vendorId
        };
    }

    // Setters
    public void SetVendorId(Guid vendorId)
    {
        ValidateVendorId(vendorId);
        VendorId = vendorId;
    }

    // Validators
    private static void ValidateVendorId(Guid vendorId)
    {
        if (vendorId == Guid.Empty)
            throw new PurchaseExceptions.InValidVendorIdException();
    }
}