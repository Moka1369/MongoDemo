using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDemo.Application.Abstractions;
using MongoDemo.Application.Exceptions;
using MongoDemo.Domain.Entities;
using MongoDemo.Infrastrukture.Settings;

namespace MongoDemo.Infrastrukture.Customers;

public class MongoCustomerRepository : ICustomerRepository
{
    private readonly IMongoCollection<CustomerDocument> _col;

    public MongoCustomerRepository(IOptions<MongoDbSettings> options)
    {
        var s = options.Value;
        var client = new MongoClient(s.ConnectionString);
        var db = client.GetDatabase(s.DatabaseName);
        _col = db.GetCollection<CustomerDocument>(s.CustomersCollectionName);
    }

    public async Task<List<Customer>> GetAllAsync(CancellationToken ct)
    {
        var docs = await _col.Find(_ => true).ToListAsync(ct);
        return docs.Select(ToDomain).ToList();
    }

    public async Task<Customer?> GetByIdAsync(string id, CancellationToken ct)
    {
        if (!ObjectId.TryParse(id,out _)) return null;  //Eine Methode hat einen out-Parameter, aber der Wert interessiert mich nicht, deshalb verwerfe ich ihn mit _.

        var doc = await _col.Find(x => x.Id == id).FirstOrDefaultAsync(ct);
        return doc is null ? null : ToDomain(doc);
    }

    public async Task<Customer> InsertAsync(Customer customer, CancellationToken ct)
    {
        var doc = ToDoc(customer);

        try
        {
            await _col.InsertOneAsync(doc, cancellationToken: ct);
        }
        catch (MongoWriteException ex) when (ex.WriteError?.Code == 11000)
        {
            throw new DuplicateEmailException(customer.Email);
        }

        customer.SetId(doc.Id!);
        return customer;
    }

    public async Task<bool> UpdateAsync(string id, Customer customer, CancellationToken ct)
    {
        var doc = ToDoc(customer);
        doc.Id = id;

        try
        {
            var res = await _col.ReplaceOneAsync(x => x.Id == id, doc, cancellationToken: ct);
            return res.MatchedCount > 0;
        }
        catch (MongoWriteException ex) when (ex.WriteError?.Code == 11000)
        {
            throw new DuplicateEmailException(customer.Email);
        }
    }

    public async Task<bool> DeleteAsync(string id, CancellationToken ct)
    {
        var res = await _col.DeleteOneAsync(x => x.Id == id, ct);
        return res.DeletedCount > 0;
    }

    private static CustomerDocument ToDoc(Customer c) => new()
    {
        Id = string.IsNullOrWhiteSpace(c.Id) ? null : c.Id,
        FirstName = c.FirstName,
        LastName = c.LastName,
        Email = c.Email,
        CreatedAtUtc = c.CreatedAtUtc
    };

    private static Customer ToDomain(CustomerDocument d)
    {
        var c = Customer.Create(d.FirstName, d.LastName, d.Email);
        c.SetId(d.Id ?? string.Empty);
        // createdAt setzen wir “wie ist”
        // Domain erlaubt nur private set, daher lassen wir es so,
        // oder erweitern Domain später sauber um CreatedAt-Set (wenn du willst).
        return c;
    }
}