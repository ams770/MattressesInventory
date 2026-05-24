using MediatR;
using Inventory.Domain.Interfaces;

namespace Inventory.Application.LocationPoints.Queries;

public record GetLocationPointByIdQuery(Guid Id) : IRequest<LocationPointDto?>;

public class GetLocationPointByIdQueryHandler(ILocationPointRepository repository)
    : IRequestHandler<GetLocationPointByIdQuery, LocationPointDto?>
{
    public async Task<LocationPointDto?> Handle(GetLocationPointByIdQuery request, CancellationToken ct)
    {
        var locationPoint = await repository.GetByIdAsync(request.Id, ct);
        if (locationPoint == null) return null;

        return new LocationPointDto(locationPoint.Id, locationPoint.Name, locationPoint.CreatedAt);
    }
}
