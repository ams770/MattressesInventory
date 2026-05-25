namespace Inventory.Domain.Exceptions;

public static class InvoiceProductExceptions
{
    public class InValidSellingPriceException() : DomainException("InvalidInvoiceProductSellingPrice");
    public class InValidActualSellingPriceException() : DomainException("InvalidInvoiceProductActualSellingPrice");
    public class InValidInvoiceIdException() : DomainException("InvalidInvoiceProductInvoiceId");
    public class InValidProductIdException() : DomainException("InvalidInvoiceProductProductId");
    public class InValidStockProductIdException() : DomainException("InvalidInvoiceProductStockProductId");
}
