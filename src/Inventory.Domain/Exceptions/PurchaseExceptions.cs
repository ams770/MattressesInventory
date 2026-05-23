namespace Inventory.Domain.Exceptions;

public static class PurchaseExceptions
{
    public class InValidVendorIdException() : DomainException("InvalidPurchaseVendorId");
}
