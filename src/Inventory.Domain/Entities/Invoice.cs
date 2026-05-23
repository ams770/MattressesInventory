using Inventory.Domain.Exceptions;

namespace Inventory.Domain.Entities;

public class Invoice : Entity<Guid>
{
    public Guid ClientId { get; private set; }
    public Client Client { get; private set; } = null!;

    private Invoice()
    {
    }

    public static Invoice Create(Guid clientId)
    {
        ValidateClientId(clientId);

        return new Invoice
        {
            Id = Guid.NewGuid(),
            ClientId = clientId
        };
    }

    // Setters
    public void SetClientId(Guid clientId)
    {
        ValidateClientId(clientId);
        ClientId = clientId;
    }

    // Validators
    private static void ValidateClientId(Guid clientId)
    {
        if (clientId == Guid.Empty)
            throw new InvoiceExceptions.InValidClientIdException();
    }
}