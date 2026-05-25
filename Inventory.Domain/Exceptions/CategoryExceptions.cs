namespace Inventory.Domain.Exceptions;

public static class CategoryExceptions
{
    public class InValidNameException() : DomainException("InvalidCategoryName");
    public class InValidDescriptionException() : DomainException("InvalidCategoryDescription");
    public class InValidImageUrlException() : DomainException("InvalidCategoryImageUrl");
}