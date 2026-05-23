using Inventory.Domain.Exceptions;

namespace Inventory.Domain.Entities;

public class Product : Entity<Guid>
{
    public string Name { get; private set; }
    public string Code { get; private set; }
    public string Barcode { get; private set; }
    public string? ImageUrl { get; private set; }
    public string? Description { get; private set; }
    public bool IsActive { get; private set; }

    private Product()
    {
    }

    public static Product Create(string code, string name, string barcode, bool isActive, string? description,
        string? imageUrl)
    {
        ValidateName(name);
        ValidateCode(code);
        ValidateBarcode(barcode);
        ValidateDescription(description);
        ValidateImageUrl(imageUrl);

        return new Product
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            Code = code,
            IsActive = isActive,
            Barcode = barcode,
            ImageUrl = imageUrl,
        };
    }

    // Setters
    public void SetName(string name)
    {
        ValidateName(name);
        Name = name;
    }

    public void SetDescription(string description)
    {
        ValidateDescription(description);
        Description = description;
    }

    public void SetImageUrl(string imageUrl)
    {
        ValidateImageUrl(imageUrl);
        ImageUrl = imageUrl;
    }

    public void SetActive(bool isActive)
    {
        IsActive = isActive;
    }

    public void SetCode(string code)
    {
        ValidateCode(code);
        Code = code;
    }

    public void SetBarcode(string barcode)
    {
        ValidateBarcode(barcode);
        Barcode = barcode;
    }

    // Validators
    private static void ValidateCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code) || code.Length > 100) throw new ProductExceptions.InValidCodeException();
    }

    private static void ValidateBarcode(string barcode)
    {
        if (string.IsNullOrWhiteSpace(barcode) || barcode.Length > 100)
            throw new ProductExceptions.InValidBarcodeException();
    }

    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > 120) throw new ProductExceptions.InValidNameException();
    }

    private static void ValidateDescription(string? description)
    {
        if (!string.IsNullOrEmpty(description) && description.Length > 240)
            throw new ProductExceptions.InValidDescriptionException();
    }

    private static void ValidateImageUrl(string? url)
    {
        if (string.IsNullOrEmpty(url)) return;
        var isValidUrl = Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
                         && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

        if (!isValidUrl)
        {
            throw new ProductExceptions.InValidImageUrlException();
        }
    }
}