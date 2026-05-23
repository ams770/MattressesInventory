namespace Inventory.Domain.Exceptions;

public static partial class ProductExceptions
{
    public class InValidCodeException() : DomainException("InvalidProductCode");
    public class InValidBarcodeException() : DomainException("InvalidProductBarcode");
    public class InValidNameException() : DomainException("InvalidProductName");
    public class InValidDescriptionException() : DomainException("InvalidProductDescription");
    public class InValidImageUrlException() : DomainException("InvalidProductImageUrl");
}