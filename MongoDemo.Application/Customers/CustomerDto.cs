namespace MongoDemo.Application.Customers;

public record CustomerDto(
    string Id,
    string FirstName,
    string LastName,
    string Email,
    DateTime CreatedAtUtc);