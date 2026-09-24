using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WarehouseManager.Application.Abstractions.Persistence;
using WarehouseManager.Infrastructure.Persistence;
using WarehouseManager.Infrastructure.Persistence.Repositories;

namespace WarehouseManager.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection
       AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString =
            configuration
                .GetConnectionString(
                    "WarehouseDb");


        if (string.IsNullOrWhiteSpace(
            connectionString))
        {
            throw new InvalidOperationException(
                "Connection string 'WarehouseDb' was not found.");
        }


        services.AddSingleton(
            new SqlConnectionFactory(
                connectionString));


        services.AddSingleton<
            IMaterialRepository,
            MaterialRepository>();


        return services;
    }
}
