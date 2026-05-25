using Inventory.Application.Interfaces;
using MediatR;
using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces;

namespace Inventory.Application.LocationPoints.Commands;

public record CreateLocationPointCommand(string Name) : IRequest<Guid>;

public class CreateLocationPointCommandHandler(IUnitOfWork unitOfWork,  ILocationPointRepository repository)
    : IRequestHandler<CreateLocationPointCommand, Guid>
{
    public async Task<Guid> Handle(CreateLocationPointCommand request, CancellationToken ct)
    {
        var locationPoint = LocationPoint.Create(request.Name);
        await repository.AddAsync(locationPoint, ct);
        await unitOfWork.SaveChangesAsync(ct);
        return locationPoint.Id;
    }
}
