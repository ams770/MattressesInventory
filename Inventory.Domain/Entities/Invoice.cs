using Inventory.Domain.Enums;
using Inventory.Domain.Exceptions;

namespace Inventory.Domain.Entities;

public class Invoice : Entity<Guid>
{
    public Guid ClientId { get; private set; }
    public Client Client { get; private set; } = null!;
    public double TotalAmount { get; private set; } 
    public double TotalDiscount { get; private set; } 
    public double PaidAmount { get; private set; } 
    public InvoiceType InvoiceType { get; private set; }  

    public double TotalAmountDiscounted => TotalAmount - TotalDiscount;
    public double TotalRemaining => TotalAmountDiscounted - PaidAmount;

    private Invoice()
    {
    }

    public static Invoice Create(Guid clientId, double totalAmount = 0, double totalDiscount = 0, double paidAmount = 0, InvoiceType invoiceType = InvoiceType.Sales)
    {
        ValidateClientId(clientId);
        ValidateTotalAmount(totalAmount);
        ValidateTotalDiscount(totalDiscount, totalAmount);
        ValidatePaidAmount(paidAmount, totalAmount - totalDiscount);

        return new Invoice
        {
            Id = Guid.NewGuid(),
            ClientId = clientId,
            TotalAmount = totalAmount,
            TotalDiscount = totalDiscount,
            PaidAmount = paidAmount,
            InvoiceType = invoiceType
        };
    }

    // Setters
    public void SetClientId(Guid clientId)
    {
        ValidateClientId(clientId);
        ClientId = clientId;
    }

    public void SetTotals(double totalAmount, double totalDiscount, double paidAmount)
    {
        ValidateTotalAmount(totalAmount);
        ValidateTotalDiscount(totalDiscount, totalAmount);
        ValidatePaidAmount(paidAmount, totalAmount - totalDiscount);

        TotalAmount = totalAmount;
        TotalDiscount = totalDiscount;
        PaidAmount = paidAmount;
    }

    public void SetInvoiceType(InvoiceType invoiceType)
    {
        InvoiceType = invoiceType;
    }

    // Validators
    private static void ValidateClientId(Guid clientId)
    {
        if (clientId == Guid.Empty)
            throw new InvoiceExceptions.InValidClientIdException();
    }

    private static void ValidateTotalAmount(double totalAmount)
    {
        if (totalAmount < 0)
            throw new InvoiceExceptions.InValidTotalAmountException();
    }

    private static void ValidateTotalDiscount(double totalDiscount, double totalAmount)
    {
        if (totalDiscount < 0 || totalDiscount > totalAmount)
            throw new InvoiceExceptions.InValidTotalDiscountException();
    }

    private static void ValidatePaidAmount(double paidAmount, double totalAmountDiscounted)
    {
        if (paidAmount < 0 || paidAmount > totalAmountDiscounted)
            throw new InvoiceExceptions.InValidPaidAmountException();
    }
}