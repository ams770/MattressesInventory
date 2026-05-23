using Inventory.Domain.Enums;

namespace Inventory.Domain.Common;

public class StockProductPagedRequest : PagedRequest
{
    public StockProductStatus? Status { get; set; }
}
