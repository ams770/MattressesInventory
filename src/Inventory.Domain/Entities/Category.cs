using Inventory.Domain.Exceptions;

namespace Inventory.Domain.Entities;

public class Category : Entity<Guid>
{
    public string Name { get; private set; }
    public string? ImageUrl { get; private set; }
    public string? Description { get; private set; }
    public bool IsActive { get; private set; }

    private Category()
    {
    }


    public static Category Create(string name, string imageUrl, string description, bool isActive)
    {
        ValidateName(name);

        var cateory = new Category
        {
            Id = Guid.NewGuid(),
            Name = name,
            ImageUrl = imageUrl,
            Description = description,
            IsActive = isActive
        };

        return cateory;
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
    

    // Validators
    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > 60) throw new CategoryExceptions.InValidNameException();
    }

    private static void ValidateDescription(string? description)
    {
        if (!string.IsNullOrEmpty(description) && description.Length > 120)
            throw new CategoryExceptions.InValidDescriptionException();
    }

    private static void ValidateImageUrl(string? url)
    {
        if (string.IsNullOrEmpty(url)) return;
        var isValidUrl = Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
                         && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

        if (!isValidUrl)
        {
            throw new CategoryExceptions.InValidImageUrlException();
        }
    }
}