using MediatR;
using Inventory.Domain.Interfaces;

namespace Inventory.Application.Clients.Queries;

public record GetClientByIdQuery(Guid Id) : IRequest<ClientDto?>;

public class GetClientByIdQueryHandler(IClientRepository clientRepository)
    : IRequestHandler<GetClientByIdQuery, ClientDto?>
{
    public async Task<ClientDto?> Handle(GetClientByIdQuery request, CancellationToken ct)
    {
        var client = await clientRepository.GetByIdAsync(request.Id, ct);
        if (client == null) return null;

        return new ClientDto(client.Id, client.Name, client.PhoneNumber, client.CreatedAt);
    }
}
