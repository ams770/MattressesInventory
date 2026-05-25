using Inventory.Application.Interfaces;
using MediatR;
using Inventory.Domain.Entities;
using Inventory.Domain.Enums;
using Inventory.Domain.Interfaces;
using Inventory.Domain.Exceptions;

namespace Inventory.Application.Invoices.Commands;

public record CreateInvoiceProductCommand(
    Guid ProductId,
    double SoldByPrice,
    int Qty);

public record CreateInvoiceCommand(
    Guid ClientId,
    double PaidAmount,
    InvoiceType InvoiceType,
    List<CreateInvoiceProductCommand> Products) : IRequest<Guid>;

public class CreateInvoiceCommandHandler(
    IInvoiceRepository invoiceRepository,
    IInvoiceProductRepository invoiceProductRepository,
    IStockProductRepository stockProductRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateInvoiceCommand, Guid>
{
    public async Task<Guid> Handle(CreateInvoiceCommand request, CancellationToken ct)
    {
        double totalAmount = 0;
        double totalDiscount = 0;

        var stockProductsByProduct = new Dictionary<Guid, List<StockProduct>>();

        foreach (var prod in request.Products)
        {
            var inStockCount = await stockProductRepository.GetCountByProductIdAndStatusAsync(prod.ProductId, StockProductStatus.InStock, ct);
            if (inStockCount < prod.Qty)
            {
                throw new StockProductExceptions.InsufficientStockException();
            }

            var availableStock = await stockProductRepository.GetAvailableProductsAsync(prod.ProductId, prod.Qty, ct);
            stockProductsByProduct[prod.ProductId] = availableStock;

            foreach (var stockProduct in availableStock)
            {
                totalAmount += stockProduct.SellingPrice;
                totalDiscount += stockProduct.SellingPrice - prod.SoldByPrice;
            }
        }

        var invoice = Invoice.Create(
            request.ClientId,
            totalAmount,
            totalDiscount,
            request.PaidAmount,
            request.InvoiceType);

        await invoiceRepository.AddAsync(invoice, ct);

        foreach (var prod in request.Products)
        {
            var availableStock = stockProductsByProduct[prod.ProductId];
            foreach (var stockProduct in availableStock)
            {
                var invoiceProduct = InvoiceProduct.Create(
                    stockProduct.SellingPrice,
                    prod.SoldByPrice,
                    invoice.Id,
                    prod.ProductId,
                    stockProduct.Id);

                await invoiceProductRepository.AddAsync(invoiceProduct, ct);

                stockProduct.SetStatus(StockProductStatus.Sold);
                stockProduct.SetSoldAt(DateTime.UtcNow);
                await stockProductRepository.UpdateAsync(stockProduct, ct);
            }
        }

        await unitOfWork.SaveChangesAsync(ct);
        return invoice.Id;
    }
}
