using Inventory.Application.Interfaces;
using MediatR;
using Inventory.Domain.Interfaces;

namespace Inventory.Application.LocationPoints.Commands;

public record UpdateLocationPointCommand(Guid Id, string Name) : IRequest;

public class UpdateLocationPointCommandHandler(ILocationPointRepository repository, IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateLocationPointCommand>
{
    public async Task Handle(UpdateLocationPointCommand request, CancellationToken ct)
    {
        var locationPoint = await repository.GetByIdAsync(request.Id, ct);
        if (locationPoint == null) throw new KeyNotFoundException($"LocationPoint with ID {request.Id} was not found.");

        locationPoint.SetName(request.Name);
        await repository.UpdateAsync(locationPoint, ct);
        await unitOfWork.SaveChangesAsync(ct);
    }
}
