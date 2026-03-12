namespace MongoDemo.Domain.Entities;

public class Customer
{
    public string Id { get; private set; } = string.Empty;

    public string FirstName { get; private set; } = string.Empty;
    public string LastName  { get; private set; } = string.Empty;
    public string Email     { get; private set; } = string.Empty;

    public DateTime CreatedAtUtc { get; private set; } = DateTime.UtcNow;

    // Factory method (EN: create method)
    public static Customer Create(string firstName, string lastName, string email)
    {
        return new Customer
        {
            FirstName = firstName.Trim(),
            LastName  = lastName.Trim(),
            Email     = NormalizeEmail(email),
            CreatedAtUtc = DateTime.UtcNow
        };
    }

    public void Update(string firstName, string lastName, string email)
    {
        FirstName = firstName.Trim();
        LastName  = lastName.Trim();
        Email     = NormalizeEmail(email);
    }

    public void SetId(string id) => Id = id;

    private static string NormalizeEmail(string email)
        => email.Trim().ToLowerInvariant();
}