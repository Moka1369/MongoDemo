using MediatR;
using MongoDemo.Application.Abstractions;
using MongoDemo.Domain.Entities;

namespace MongoDemo.Application.Customers.Create;

public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, CustomerDto>
{
    private readonly ICustomerRepository _repo;

    public CreateCustomerHandler(ICustomerRepository repo) => _repo = repo;

    public async Task<CustomerDto> Handle(CreateCustomerCommand request, CancellationToken ct)
    {
        var customer = Customer.Create(request.FirstName, request.LastName, request.Email);
        var saved = await _repo.InsertAsync(customer, ct);

        return new CustomerDto(saved.Id, saved.FirstName, saved.LastName, saved.Email, saved.CreatedAtUtc);
    }
}