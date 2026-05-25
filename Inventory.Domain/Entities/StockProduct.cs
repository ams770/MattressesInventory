using Inventory.Domain.Enums;
using Inventory.Domain.Exceptions;

namespace Inventory.Domain.Entities;

public class StockProduct : Entity<Guid>
{
    public double ProductPrice { get; private set; }
    public double ShippingCost { get; private set; }
    public double SellingPrice { get; private set; }
    public Guid PurchaseId { get; private set; }
    public Purchase Purchase { get; private set; } = null!;
    public Guid ProductId { get; private set; }
    public Product Product { get; private set; } = null!;
    public Guid? LocationPointId { get; private set; }
    public LocationPoint? LocationPoint { get; private set; }
    public StockProductStatus Status { get; private set; }
    public DateTime? SoldAt { get; private set; }
    public DateTime? ReturnedAt { get; private set; }

    private StockProduct()
    {
    }

    public static StockProduct Create(double productPrice, double shippingCost, double sellingPrice, Guid purchaseId, Guid productId, Guid? locationPointId, StockProductStatus status)
    {
        ValidateProductPrice(productPrice);
        ValidateShippingCost(shippingCost);
        ValidateSellingPrice(sellingPrice);
        ValidatePurchaseId(purchaseId);
        ValidateProductId(productId);
        ValidateLocationPointId(locationPointId);

        return new StockProduct
        {
            Id = Guid.NewGuid(),
            ProductPrice = productPrice,
            ShippingCost = shippingCost,
            SellingPrice = sellingPrice,
            PurchaseId = purchaseId,
            ProductId = productId,
            LocationPointId = locationPointId,
            Status = status
        };
    }

    // Setters
    public void SetProductPrice(double productPrice)
    {
        ValidateProductPrice(productPrice);
        ProductPrice = productPrice;
    }

    public void SetShippingCost(double shippingCost)
    {
        ValidateShippingCost(shippingCost);
        ShippingCost = shippingCost;
    }

    public void SetSellingPrice(double sellingPrice)
    {
        ValidateSellingPrice(sellingPrice);
        SellingPrice = sellingPrice;
    }

    public void SetPurchaseId(Guid purchaseId)
    {
        ValidatePurchaseId(purchaseId);
        PurchaseId = purchaseId;
    }

    public void SetProductId(Guid productId)
    {
        ValidateProductId(productId);
        ProductId = productId;
    }

    public void SetLocationPointId(Guid? locationPointId)
    {
        ValidateLocationPointId(locationPointId);
        LocationPointId = locationPointId;
    }

    public void SetStatus(StockProductStatus status)
    {
        Status = status;
    }

    public void SetSoldAt(DateTime? soldAt)
    {
        SoldAt = soldAt;
    }

    public void SetReturnedAt(DateTime? returnedAt)
    {
        ReturnedAt = returnedAt;
    }

    // Validators
    private static void ValidateProductPrice(double productPrice)
    {
        if (productPrice < 0)
            throw new StockProductExceptions.InValidProductPriceException();
    }

    private static void ValidateShippingCost(double shippingCost)
    {
        if (shippingCost < 0)
            throw new StockProductExceptions.InValidShippingCostException();
    }

    private static void ValidateSellingPrice(double sellingPrice)
    {
        if (sellingPrice < 0)
            throw new StockProductExceptions.InValidSellingPriceException();
    }

    private static void ValidatePurchaseId(Guid purchaseId)
    {
        if (purchaseId == Guid.Empty)
            throw new StockProductExceptions.InValidPurchaseIdException();
    }

    private static void ValidateProductId(Guid productId)
    {
        if (productId == Guid.Empty)
            throw new StockProductExceptions.InValidProductIdException();
    }

    private static void ValidateLocationPointId(Guid? locationPointId)
    {
        if (locationPointId.HasValue && locationPointId.Value == Guid.Empty)
            throw new StockProductExceptions.InValidLocationPointIdException();
    }
}