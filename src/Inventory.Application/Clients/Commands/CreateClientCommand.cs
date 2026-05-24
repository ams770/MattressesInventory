using MediatR;
using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces;

namespace Inventory.Application.Clients.Commands;

public record CreateClientCommand(string Name, string PhoneNumber) : IRequest<Guid>;

public class CreateClientCommandHandler(IClientRepository clientRepository) : IRequestHandler<CreateClientCommand, Guid>
{
    public async Task<Guid> Handle(CreateClientCommand request, CancellationToken ct)
    {
        var client = Client.Create(request.Name, request.PhoneNumber);
        await clientRepository.AddAsync(client, ct);
        return client.Id;
    }
}
