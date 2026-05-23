using Inventory.Domain.Exceptions;

namespace Inventory.Domain.Entities;

public class InvoiceProduct : Entity<Guid>
{
    public double SellingPrice { get; private set; }
    public double ActualSellingPrice { get; private set; }
    public Guid InvoiceId { get; private set; }
    public Guid ProductId { get; private set; }
    public Product Product { get; private set; } = null!;
    public Guid StockProductId { get; private set; }
    public StockProduct StockProduct { get; private set; } = null!;

    private InvoiceProduct()
    {
    }

    public static InvoiceProduct Create(double sellingPrice, double actualSellingPrice, Guid invoiceId, Guid productId, Guid stockProductId)
    {
        ValidateSellingPrice(sellingPrice);
        ValidateActualSellingPrice(actualSellingPrice);
        ValidateInvoiceId(invoiceId);
        ValidateProductId(productId);
        ValidateStockProductId(stockProductId);

        return new InvoiceProduct
        {
            Id = Guid.NewGuid(),
            SellingPrice = sellingPrice,
            ActualSellingPrice = actualSellingPrice,
            InvoiceId = invoiceId,
            ProductId = productId,
            StockProductId = stockProductId
        };
    }

    // Setters
    public void SetSellingPrice(double sellingPrice)
    {
        ValidateSellingPrice(sellingPrice);
        SellingPrice = sellingPrice;
    }

    public void SetActualSellingPrice(double actualSellingPrice)
    {
        ValidateActualSellingPrice(actualSellingPrice);
        ActualSellingPrice = actualSellingPrice;
    }

    public void SetInvoiceId(Guid invoiceId)
    {
        ValidateInvoiceId(invoiceId);
        InvoiceId = invoiceId;
    }

    public void SetProductId(Guid productId)
    {
        ValidateProductId(productId);
        ProductId = productId;
    }

    public void SetStockProductId(Guid stockProductId)
    {
        ValidateStockProductId(stockProductId);
        StockProductId = stockProductId;
    }

    // Validators
    private static void ValidateSellingPrice(double sellingPrice)
    {
        if (sellingPrice < 0)
            throw new InvoiceProductExceptions.InValidSellingPriceException();
    }

    private static void ValidateActualSellingPrice(double actualSellingPrice)
    {
        if (actualSellingPrice < 0)
            throw new InvoiceProductExceptions.InValidActualSellingPriceException();
    }

    private static void ValidateInvoiceId(Guid invoiceId)
    {
        if (invoiceId == Guid.Empty)
            throw new InvoiceProductExceptions.InValidInvoiceIdException();
    }

    private static void ValidateProductId(Guid productId)
    {
        if (productId == Guid.Empty)
            throw new InvoiceProductExceptions.InValidProductIdException();
    }

    private static void ValidateStockProductId(Guid stockProductId)
    {
        if (stockProductId == Guid.Empty)
            throw new InvoiceProductExceptions.InValidStockProductIdException();
    }
}