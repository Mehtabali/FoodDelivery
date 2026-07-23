using Messaging.Contracts.Common;

namespace Messaging.Contracts.Events
{
    public sealed record InventoryReservationFailedEvent : IntegrationMessage
    {
        public required Guid OrderId { get; init; }
        public required string Reason { get; init; }

    }
}