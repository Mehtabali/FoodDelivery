using Messaging.Contracts.Common;

namespace Messaging.Contracts.Events;
public sealed record InventoryReservedEvent : IntegrationMessage
{
    public required Guid OrderId { get; init; }
    public required Guid ReservationId { get; init; }
}
