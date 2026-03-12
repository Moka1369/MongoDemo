using MediatR;
using MongoDemo.Application.Abstractions;

namespace MongoDemo.Application.Customers.Delete;

public class DeleteByIdHandler : IRequestHandler<DeleteByIdCommand, bool>
{
    private readonly ICustomerRepository _repo;
    public DeleteByIdHandler(ICustomerRepository repo)
    {
        _repo = repo;
    }

    public async Task<bool> Handle(DeleteByIdCommand request, CancellationToken token)
    {
        return await _repo.DeleteAsync(request.Id, token);
    }
}