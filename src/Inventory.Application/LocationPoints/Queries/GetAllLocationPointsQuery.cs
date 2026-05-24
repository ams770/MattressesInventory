using MediatR;
using Inventory.Domain.Common;
using Inventory.Domain.Interfaces;

namespace Inventory.Application.LocationPoints.Queries;

public record GetAllLocationPointsQuery(PagedRequest Request) : IRequest<PagedResult<LocationPointDto>>;

public class GetAllLocationPointsQueryHandler(ILocationPointRepository repository)
    : IRequestHandler<GetAllLocationPointsQuery, PagedResult<LocationPointDto>>
{
    public async Task<PagedResult<LocationPointDto>> Handle(GetAllLocationPointsQuery request, CancellationToken ct)
    {
        var result = await repository.GetAllAsync(request.Request, ct);
        var dtos = result.Items.Select(x => new LocationPointDto(x.Id, x.Name, x.CreatedAt)).ToList();

        return new PagedResult<LocationPointDto>(dtos, result.TotalCount, result.PageNumber, result.PageSize);
    }
}
