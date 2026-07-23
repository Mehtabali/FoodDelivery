namespace OrderService.Infrastructure.Messaging;
/// <summary>
/// Outbox table would store serialized command/event.
// For Example 
/// Id              = ReserveInventoryCommand ka MessageId
///Type            = ReserveInventoryCommand
///Content = JSON payload
///OccurredOnUtc = message creation time
///ProcessedOnUtc  = null
/// </summary>
public sealed class OutboxMessage
{
    public Guid Id { get; private set; }

    public Guid CorrelationId { get; private set; }
    
    public string Type { get; private set; } = string.Empty;

    public string Content { get; private set; } = string.Empty;

    public DateTime OccurredOnUtc { get; private set; }
    
    public DateTime? ProcessedOnUtc { get; private set; }

    public string? Error { get; private set; } = string.Empty;
    
    public int RetryCount { get; private set; }

    private OutboxMessage()
    {
    }

    public OutboxMessage(
        Guid id,
        Guid correlationId,
        string type,
        string content,
        DateTime occurredOnUtc)
    {
        Id = id;
        CorrelationId = correlationId;
        Type = type;
        Content = content;
        OccurredOnUtc = occurredOnUtc;
    }

    public void MarkAsProcessed()
    {
        ProcessedOnUtc = DateTime.UtcNow;
        Error = null;
    }

    public void MarkAsFailed(string error)
    {
        RetryCount++;
        Error = error;
    }
}
