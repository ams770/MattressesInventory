namespace Inventory.Domain.Exceptions;

public static class StockProductExceptions
{
    public class InValidProductPriceException() : DomainException("InvalidStockProductProductPrice");
    public class InValidShippingCostException() : DomainException("InvalidStockProductShippingCost");
    public class InValidSellingPriceException() : DomainException("InvalidStockProductSellingPrice");
    public class InValidPurchaseIdException() : DomainException("InvalidStockProductPurchaseId");
    public class InValidProductIdException() : DomainException("InvalidStockProductProductId");
    public class InValidLocationPointIdException() : DomainException("InvalidStockProductLocationPointId");
}
