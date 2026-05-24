using MediatR;
using Inventory.Domain.Common;
using Inventory.Domain.Interfaces;

namespace Inventory.Application.LocationPoints.Queries;

public record GetAllLocationPointsQuery(string? SearchTerm, int PageNumber = 1, int PageSize = 10) : IRequest<PagedResult<LocationPointDto>>;

public class GetAllLocationPointsQueryHandler(ILocationPointRepository repository)
    : IRequestHandler<GetAllLocationPointsQuery, PagedResult<LocationPointDto>>
{
    public async Task<PagedResult<LocationPointDto>> Handle(GetAllLocationPointsQuery request, CancellationToken ct)
    {
        var pagedRequest = new PagedRequest
        {
            SearchTerm = request.SearchTerm,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };

        var result = await repository.GetAllAsync(pagedRequest, ct);
        var dtos = result.Items.Select(x => new LocationPointDto(x.Id, x.Name, x.CreatedAt)).ToList();

        return new PagedResult<LocationPointDto>(dtos, result.TotalCount, result.PageNumber, result.PageSize);
    }
}
