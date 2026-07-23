using Azure.Messaging.ServiceBus;
using OrderService.Application.Abstractions.Messaging;

namespace OrderService.Infrastructure.Messaging;

internal sealed class AzureServiceBusMessagePublisher(ServiceBusSender sender) : IMessagePublisher
{
    public async Task PublishAsync(
        Guid messageId,
        Guid correlationId,
        string messageType,
        string content,
        CancellationToken cancellationToken)
    {
        var message = new ServiceBusMessage(content)
        {
            MessageId = messageId.ToString(),
            CorrelationId = correlationId.ToString(),
            Subject = messageType,
            ContentType = "application/json"
        };

        message.ApplicationProperties["messageType"] = messageType;

        await sender.SendMessageAsync(message, cancellationToken);
    }
}
