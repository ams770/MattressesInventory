using MediatR;
using Inventory.Domain.Common;
using Inventory.Domain.Interfaces;

namespace Inventory.Application.Invoices.Queries;

public record GetAllInvoicesQuery(
    string? SearchTerm,
    int PageNumber = 1,
    int PageSize = 10,
    Guid? ClientId = null,
    DateTime? FromDate = null,
    DateTime? ToDate = null) : IRequest<PagedResult<InvoiceDto>>;

public class GetAllInvoicesQueryHandler(IInvoiceRepository invoiceRepository)
    : IRequestHandler<GetAllInvoicesQuery, PagedResult<InvoiceDto>>
{
    public async Task<PagedResult<InvoiceDto>> Handle(GetAllInvoicesQuery request, CancellationToken ct)
    {
        var pagedRequest = new InvoicePagedRequest
        {
            SearchTerm = request.SearchTerm,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            ClientId = request.ClientId,
            FromDate = request.FromDate,
            ToDate = request.ToDate
        };

        var result = await invoiceRepository.GetAllAsync(pagedRequest, ct);
        var dtos = result.Items.Select(invoice => new InvoiceDto(
            invoice.Id,
            invoice.ClientId,
            invoice.TotalAmount,
            invoice.TotalDiscount,
            invoice.PaidAmount,
            invoice.TotalAmountDiscounted,
            invoice.TotalRemaining,
            invoice.InvoiceType,
            invoice.CreatedAt)).ToList();

        return new PagedResult<InvoiceDto>(dtos, result.TotalCount, result.PageNumber, result.PageSize);
    }
}
