using MediatR;
using MongoDemo.Application.Abstractions;

namespace MongoDemo.MongoDemo.Application.Customers.Update;

public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, bool>
{
    private readonly ICustomerRepository _repo;
    public UpdateCustomerHandler(ICustomerRepository repo)
    {
        _repo = repo;
    }

    public async Task<bool> Handle(UpdateCustomerCommand request, CancellationToken token)
    {
        var customer = await _repo.GetByIdAsync(request.Id, token);
        if (customer is null) return false;

        customer.Update(request.FirstName,
        request.LastName,
        request.Email);
        return await _repo.UpdateAsync(request.Id, customer, token);
    }
}