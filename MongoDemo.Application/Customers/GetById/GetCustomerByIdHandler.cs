

using MediatR;
using MongoDemo.Application.Abstractions;
using MongoDemo.Application.Customers;

namespace MongoDemo.MongoDemo.Application.Customers.GetById;

public class GetCustomerByIdHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDto?>
{
    private readonly ICustomerRepository _repo;
    public GetCustomerByIdHandler(ICustomerRepository repo)
    {
        _repo = repo;
    }

    public async Task<CustomerDto?> Handle(GetCustomerByIdQuery request, CancellationToken token)
    {
        var customer = await _repo.GetByIdAsync(request.Id, token);

        if (customer is null) return null;

        return new CustomerDto(customer.Id,
        customer.FirstName,
        customer.LastName,
        customer.Email,
        customer.CreatedAtUtc);
    }
}