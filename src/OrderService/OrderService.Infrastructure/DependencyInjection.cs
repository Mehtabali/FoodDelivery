using Azure.Messaging.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrderService.Application.Abstractions.Messaging;
using OrderService.Application.Abstractions.Persistence;
using OrderService.Infrastructure.Messaging;
using OrderService.Infrastructure.Messaging.Outbox;
using OrderService.Infrastructure.Persistence;

namespace OrderService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddOrderServiceInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString =
             configuration.GetConnectionString("OrderDB")
             ?? throw new InvalidOperationException(
                 "OrderDatabase connection string is missing.");

        services.AddDbContext<OrderDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IOrderDbContext>(provider =>
            provider.GetRequiredService<OrderDbContext>());

        services.Configure<AzureServiceBusOptions>(
            configuration.GetSection(AzureServiceBusOptions.SectionName));

        services.AddSingleton(provider =>
        {
            var options = provider.GetRequiredService<IOptions<AzureServiceBusOptions>>().Value;

            if (string.IsNullOrWhiteSpace(options.ConnectionString))
            {
                throw new InvalidOperationException(
                    "Azure Service Bus connection string is missing. Configure 'AzureServiceBus:ConnectionString'.");
            }

            return new ServiceBusClient(options.ConnectionString);
        });

        services.AddSingleton(provider =>
        {
            var options = provider.GetRequiredService<IOptions<AzureServiceBusOptions>>().Value;
            var client = provider.GetRequiredService<ServiceBusClient>();

            return client.CreateSender(options.ReserveInventoryQueueName);
        });

        services.AddSingleton<IMessagePublisher, AzureServiceBusMessagePublisher>();
        services.AddHostedService<OutboxProcessor>();

        return services;
    }
}
