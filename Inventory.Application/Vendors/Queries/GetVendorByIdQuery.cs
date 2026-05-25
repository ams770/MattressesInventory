using MediatR;
using Inventory.Domain.Interfaces;

namespace Inventory.Application.Vendors.Queries;

public record GetVendorByIdQuery(Guid Id) : IRequest<VendorDto?>;

public class GetVendorByIdQueryHandler(IVendorRepository repository)
    : IRequestHandler<GetVendorByIdQuery, VendorDto?>
{
    public async Task<VendorDto?> Handle(GetVendorByIdQuery request, CancellationToken ct)
    {
        var vendor = await repository.GetByIdAsync(request.Id, ct);
        if (vendor == null) return null;

        return new VendorDto(vendor.Id, vendor.Name, vendor.CreatedAt);
    }
}
