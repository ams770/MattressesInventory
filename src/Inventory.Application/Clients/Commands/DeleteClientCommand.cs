using MediatR;
using Inventory.Domain.Interfaces;

namespace Inventory.Application.Clients.Commands;

public record DeleteClientCommand(Guid Id) : IRequest;

public class DeleteClientCommandHandler(IClientRepository clientRepository) : IRequestHandler<DeleteClientCommand>
{
    public async Task Handle(DeleteClientCommand request, CancellationToken ct)
    {
        var client = await clientRepository.GetByIdAsync(request.Id, ct);
        if (client == null) throw new KeyNotFoundException($"Client with ID {request.Id} was not found.");

        await clientRepository.DeleteAsync(client, ct);
    }
}
