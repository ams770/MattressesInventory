namespace Inventory.Domain.Common;

public class InvoicePagedRequest : PagedRequest
{
    public Guid? ClientId { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}
