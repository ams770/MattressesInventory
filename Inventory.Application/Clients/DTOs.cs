namespace Inventory.Application.Clients;

public record ClientDto(Guid Id, string Name, string PhoneNumber, DateTime CreatedAt);
