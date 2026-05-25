using MediatR;
using Inventory.Application.Interfaces;
using Inventory.Domain.Interfaces;

namespace Inventory.Application.Vendors.Commands;

public record UpdateVendorCommand(Guid Id, string Name) : IRequest;

public class UpdateVendorCommandHandler(IVendorRepository repository, IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateVendorCommand>
{
    public async Task Handle(UpdateVendorCommand request, CancellationToken ct)
    {
        var vendor = await repository.GetByIdAsync(request.Id, ct);
        if (vendor == null)
            throw new KeyNotFoundException($"Vendor with ID {request.Id} was not found.");

        vendor.SetName(request.Name);
        await repository.UpdateAsync(vendor, ct);
        await unitOfWork.SaveChangesAsync(ct);
    }
}
