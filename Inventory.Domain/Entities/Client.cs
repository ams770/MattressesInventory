using Inventory.Domain.Exceptions;

namespace Inventory.Domain.Entities;

public class Client : Entity<Guid>
{
    public string Name { get; private set; }
    public string PhoneNumber { get; private set; }

    private Client()
    {
    }

    public static Client Create(string name, string phoneNumber)
    {
        ValidateName(name);
        ValidatePhoneNumber(phoneNumber);

        return new Client
        {
            Id = Guid.NewGuid(),
            Name = name,
            PhoneNumber = phoneNumber
        };
    }

    // Setters
    public void SetName(string name)
    {
        ValidateName(name);
        Name = name;
    }

    public void SetPhoneNumber(string phoneNumber)
    {
        ValidatePhoneNumber(phoneNumber);
        PhoneNumber = phoneNumber;
    }

    // Validators
    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > 100)
            throw new ClientExceptions.InValidNameException();
    }

    private static void ValidatePhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber) || phoneNumber.Length > 20)
            throw new ClientExceptions.InValidPhoneNumberException();
    }
}