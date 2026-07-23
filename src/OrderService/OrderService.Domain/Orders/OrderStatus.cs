namespace OrderService.Domain.Orders;

public enum OrderStatus
{
    Pending = 1,
    InventoryReservationPending = 2,
    InventoryReserved = 3,
    InventoryReservationFailed = 4,
    Confirmed = 5,
    Cancelled = 6
}
