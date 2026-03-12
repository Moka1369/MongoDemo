using MediatR;
using MongoDemo.Application.Abstractions;

namespace MongoDemo.Application.Customers.GetAll;

public class GetCustomersHandler : IRequestHandler<GetCustomersQuery, List<CustomerDto>>
{
    private readonly ICustomerRepository _repo;

    public GetCustomersHandler(ICustomerRepository repo) => _repo = repo;

    public async Task<List<CustomerDto>> Handle(GetCustomersQuery request, CancellationToken ct)
    {
        var items = await _repo.GetAllAsync(ct);
        return items.Select(x => new CustomerDto(x.Id, x.FirstName, x.LastName, x.Email, x.CreatedAtUtc)).ToList();
    }
}