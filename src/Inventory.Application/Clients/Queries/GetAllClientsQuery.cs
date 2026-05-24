using MediatR;
using Inventory.Domain.Common;
using Inventory.Domain.Interfaces;

namespace Inventory.Application.Clients.Queries;

public record GetAllClientsQuery(PagedRequest Request) : IRequest<PagedResult<ClientDto>>;


public class GetAllClientsQueryHandler(IClientRepository clientRepository)
    : IRequestHandler<GetAllClientsQuery, PagedResult<ClientDto>>
{
    public async Task<PagedResult<ClientDto>> Handle(GetAllClientsQuery request, CancellationToken ct)
    {
        var result = await clientRepository.GetAllAsync(request.Request, ct);
        var dtos = result.Items.Select(client => new ClientDto(client.Id, client.Name, client.PhoneNumber, client.CreatedAt)).ToList();

        return new PagedResult<ClientDto>(dtos, result.TotalCount, result.PageNumber, result.PageSize);
    }
}
