namespace Inventory.Domain.Exceptions;

public static class ClientExceptions
{
    public class InValidNameException() : DomainException("InvalidClientName");
    public class InValidPhoneNumberException() : DomainException("InvalidClientPhoneNumber");
}
