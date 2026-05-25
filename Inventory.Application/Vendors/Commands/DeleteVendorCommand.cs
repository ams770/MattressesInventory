using MediatR;
using Inventory.Application.Interfaces;
using Inventory.Domain.Interfaces;

namespace Inventory.Application.Vendors.Commands;

public record DeleteVendorCommand(Guid Id) : IRequest;

public class DeleteVendorCommandHandler(IVendorRepository repository, IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteVendorCommand>
{
    public async Task Handle(DeleteVendorCommand request, CancellationToken ct)
    {
        var vendor = await repository.GetByIdAsync(request.Id, ct);
        if (vendor == null)
            throw new KeyNotFoundException($"Vendor with ID {request.Id} was not found.");

        await repository.DeleteAsync(vendor, ct);
        await unitOfWork.SaveChangesAsync(ct);
    }
}
