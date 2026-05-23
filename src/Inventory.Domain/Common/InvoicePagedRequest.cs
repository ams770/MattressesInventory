namespace Inventory.Domain.Common;

public class InvoicePagedRequest : PagedRequest
{
    public Guid? ClientId { get; set; }
}
