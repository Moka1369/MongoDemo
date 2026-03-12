

using MediatR;
using MongoDemo.Application.Customers;

namespace MongoDemo.MongoDemo.Application.Customers.GetById;

public record GetCustomerByIdQuery(string Id) : IRequest<CustomerDto?>;
