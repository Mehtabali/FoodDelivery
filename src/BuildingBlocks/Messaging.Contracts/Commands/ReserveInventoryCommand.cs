using Messaging.Contracts.Common;

namespace Messaging.Contracts.Commands;
public sealed record ReserveInventoryCommand : IntegrationMessage
{
    public required Guid OrderId { get; init; }
    public required IReadOnlyCollection<ReserveInventoryItem> Items { get; init; }
}

public sealed record ReserveInventoryItem(Guid ProductId, int Quantity);