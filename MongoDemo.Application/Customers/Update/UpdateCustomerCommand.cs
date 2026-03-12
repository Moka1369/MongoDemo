using MediatR;

namespace MongoDemo.MongoDemo.Application.Customers.Update;

public record UpdateCustomerCommand(string Id,
string FirstName,
string LastName,
string Email) : IRequest<bool>;