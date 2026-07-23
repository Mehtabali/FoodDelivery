namespace OrderService.Application.Abstractions.Messaging;

public interface IMessagePublisher
{
    Task PublishAsync(
        Guid messageId,
        Guid correlationId,
        string messageType,
        string content,
        CancellationToken cancellationToken);
}
