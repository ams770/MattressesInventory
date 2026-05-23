namespace Inventory.Domain.Exceptions;

public static class CategoryProductExceptions
{
    public class InValidCategoryIdException() : DomainException("InvalidCategoryProductCategoryId");
    public class InValidProductIdException() : DomainException("InvalidCategoryProductProductId");
}
