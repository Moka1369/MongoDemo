using MediatR;

namespace MongoDemo.Application.Customers.GetAll;

public record GetCustomersQuery() : IRequest<List<CustomerDto>>;