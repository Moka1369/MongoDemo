using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDemo.Application.Abstractions;
using MongoDemo.Infrastrukture.Customers;
using MongoDemo.Infrastrukture.Settings;

namespace MongoDemo.Infrastrukture;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastrukture(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<MongoDbSettings>(config.GetSection("MongoDb"));

        services.AddSingleton<ICustomerRepository, MongoCustomerRepository>();
        services.AddHostedService<MongoCustomerIndexHostedService>();

        return services;
    }
}