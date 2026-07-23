namespace Messaging.Contracts.Common;

public abstract record IntegrationMessage
{
    public Guid MessageId { get; init; } = Guid.NewGuid();
    public Guid CorrelationId { get; init; }
    public DateTime OccurredOnUtc { get; set; } = DateTime.UtcNow;
}
