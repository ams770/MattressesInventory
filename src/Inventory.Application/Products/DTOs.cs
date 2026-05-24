namespace Inventory.Application.Products;

public record CategoryDto(Guid Id, string Name, string? ImageUrl, string? Description, bool IsActive, DateTime CreatedAt);

public record ProductDto(
    Guid Id,
    string Code,
    string Name,
    string Barcode,
    string? ImageUrl,
    string? Description,
    bool IsActive,
    DateTime CreatedAt);
