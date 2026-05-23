namespace Inventory.Domain.Exceptions;

public static class VendorExceptions
{
    public class InValidNameException() : DomainException("InvalidVendorName");
}
