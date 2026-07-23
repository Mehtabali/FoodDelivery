namespace InventoryService.Infrastructure.Messaging;

public sealed class InboxMessage
{
    public Guid MessageId { get; set; }

    public string MessageType { get; set; } = string.Empty;

    public DateTime ProcessedAtUtc { get; set; }
}
