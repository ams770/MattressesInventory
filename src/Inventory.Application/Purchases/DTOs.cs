using Inventory.Domain.Enums;

namespace Inventory.Application.Purchases;

public record PurchaseDto(Guid Id, Guid VendorId, DateTime CreatedAt);

public record StockProductDto(
    Guid Id,
    double ProductPrice,
    double ShippingCost,
    double SellingPrice,
    Guid PurchaseId,
    Guid ProductId,
    Guid? LocationPointId,
    StockProductStatus Status,
    DateTime? SoldAt,
    DateTime? ReturnedAt,
    DateTime CreatedAt);
