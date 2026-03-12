using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDemo.Infrastrukture.Settings;

namespace MongoDemo.Infrastrukture.Customers;

public class MongoCustomerIndexHostedService : IHostedService
{
    private readonly MongoClient _client;
    private readonly MongoDbSettings _settings;

    public MongoCustomerIndexHostedService(IOptions<MongoDbSettings> options)
    {
        _settings = options.Value;
        _client = new MongoClient(_settings.ConnectionString);
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var db = _client.GetDatabase(_settings.DatabaseName);
        var col = db.GetCollection<CustomerDocument>(_settings.CustomersCollectionName);

        var keys = Builders<CustomerDocument>.IndexKeys.Ascending(x => x.Email);
        var opts = new CreateIndexOptions { Unique = true, Name = "ux_customers_email" };

        await col.Indexes.CreateOneAsync(new CreateIndexModel<CustomerDocument>(keys, opts), cancellationToken: cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}