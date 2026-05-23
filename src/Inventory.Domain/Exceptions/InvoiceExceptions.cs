namespace Inventory.Domain.Exceptions;

public static class InvoiceExceptions
{
    public class InValidClientIdException() : DomainException("InvalidInvoiceClientId");
}
