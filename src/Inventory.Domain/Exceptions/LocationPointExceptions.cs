namespace Inventory.Domain.Exceptions;

public static class LocationPointExceptions
{
    public class InValidNameException() : DomainException("InvalidLocationPointName");
}
