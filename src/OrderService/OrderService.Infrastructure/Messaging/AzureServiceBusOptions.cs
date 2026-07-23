namespace OrderService.Infrastructure.Messaging;

public sealed class AzureServiceBusOptions
{
    public const string SectionName = "AzureServiceBus";

    public string ConnectionString { get; init; } = string.Empty;

    public string ReserveInventoryQueueName { get; init; } = "reserve-inventory";
}
