namespace Inventory.Domain.Exceptions;

public static class InvoiceExceptions
{
    public class InValidClientIdException() : DomainException("InvalidInvoiceClientId");
    public class InValidTotalAmountException() : DomainException("InvalidInvoiceTotalAmount");
    public class InValidTotalDiscountException() : DomainException("InvalidInvoiceTotalDiscount");
    public class InValidPaidAmountException() : DomainException("InvalidInvoicePaidAmount");
}
