using Inventory.Application.Interfaces;
using MediatR;
using Inventory.Domain.Interfaces;

namespace Inventory.Application.LocationPoints.Commands;

public record DeleteLocationPointCommand(Guid Id) : IRequest;

public class DeleteLocationPointCommandHandler(ILocationPointRepository repository, IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteLocationPointCommand>
{
    public async Task Handle(DeleteLocationPointCommand request, CancellationToken ct)
    {
        var locationPoint = await repository.GetByIdAsync(request.Id, ct);
        if (locationPoint == null) throw new KeyNotFoundException($"LocationPoint with ID {request.Id} was not found.");

        await repository.DeleteAsync(locationPoint, ct);
        await unitOfWork.SaveChangesAsync(ct);
    }
}
