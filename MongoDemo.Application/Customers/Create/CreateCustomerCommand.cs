using MediatR;
using MongoDemo.Application.Customers;

namespace MongoDemo.Application.Customers.Create;

public record CreateCustomerCommand(string FirstName, string LastName, string Email) 
    : IRequest<CustomerDto>;