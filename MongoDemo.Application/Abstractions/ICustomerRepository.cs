using MongoDemo.Domain.Entities;

namespace MongoDemo.Application.Abstractions;

public interface ICustomerRepository
{
    Task<List<Customer>> GetAllAsync(CancellationToken ct);
    Task<Customer?> GetByIdAsync(string id, CancellationToken ct);
    Task<Customer> InsertAsync(Customer customer, CancellationToken ct);
    Task<bool> UpdateAsync(string id, Customer customer, CancellationToken ct);
    Task<bool> DeleteAsync(string id, CancellationToken ct);
}