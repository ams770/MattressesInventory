using MediatR;
using Inventory.Domain.Common;
using Inventory.Domain.Interfaces;

namespace Inventory.Application.Clients.Queries;

public record GetAllClientsQuery(string? SearchTerm, int PageNumber = 1, int PageSize = 10) : IRequest<PagedResult<ClientDto>>;


public class GetAllClientsQueryHandler(IClientRepository clientRepository)
    : IRequestHandler<GetAllClientsQuery, PagedResult<ClientDto>>
{
    public async Task<PagedResult<ClientDto>> Handle(GetAllClientsQuery request, CancellationToken ct)
    {
        var pagedRequest = new PagedRequest
        {
            SearchTerm = request.SearchTerm,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };

        var result = await clientRepository.GetAllAsync(pagedRequest, ct);
        var dtos = result.Items.Select(client => new ClientDto(client.Id, client.Name, client.PhoneNumber, client.CreatedAt)).ToList();

        return new PagedResult<ClientDto>(dtos, result.TotalCount, result.PageNumber, result.PageSize);
    }
}
