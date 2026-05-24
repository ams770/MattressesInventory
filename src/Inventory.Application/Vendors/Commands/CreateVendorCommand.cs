using MediatR;
using Inventory.Application.Interfaces;
using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces;

namespace Inventory.Application.Vendors.Commands;

public record CreateVendorCommand(string Name) : IRequest<Guid>;

public class CreateVendorCommandHandler(IVendorRepository repository, IUnitOfWork unitOfWork)
    : IRequestHandler<CreateVendorCommand, Guid>
{
    public async Task<Guid> Handle(CreateVendorCommand request, CancellationToken ct)
    {
        var vendor = Vendor.Create(request.Name);
        await repository.AddAsync(vendor, ct);
        await unitOfWork.SaveChangesAsync(ct);
        return vendor.Id;
    }
}
