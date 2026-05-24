using MediatR;
using Inventory.Domain.Interfaces;

namespace Inventory.Application.Clients.Commands;

public record UpdateClientCommand(Guid Id, string Name, string PhoneNumber) : IRequest;

public class UpdateClientCommandHandler(IClientRepository clientRepository) : IRequestHandler<UpdateClientCommand>
{
    public async Task Handle(UpdateClientCommand request, CancellationToken ct)
    {
        var client = await clientRepository.GetByIdAsync(request.Id, ct);
        if (client == null) throw new KeyNotFoundException($"Client with ID {request.Id} was not found.");

        client.SetName(request.Name);
        client.SetPhoneNumber(request.PhoneNumber);

        await clientRepository.UpdateAsync(client, ct);
    }
}
