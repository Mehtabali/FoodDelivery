namespace OrderService.Domain.Orders;

public sealed class Order
{
    private readonly List<OrderItem> _items = [];
    public Guid Id { get; set; }

    public Guid CustomerId { get; private set; }

    public OrderStatus Status { get; private set; }

    public decimal TotalAmount { get; private set; }

    public DateTime CreatedOnUtc { get; private set; }

    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
    public Order()
    {
        
    }
    public Order(Guid customerId)
    {
        Id = Guid.NewGuid();
        CustomerId = customerId;
        Status = OrderStatus.Pending;
        CreatedOnUtc = DateTime.UtcNow;
    }
    public void Add(Guid productId, int quantity, decimal unitPrice)
    {
        if(quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(quantity),
                "Quantity must be greater than zero.");
        }

        if (unitPrice < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(unitPrice),
                "Unit price cannot be negative.");
        }

        var item = new OrderItem(
           Guid.NewGuid(),
           Id,
           productId,
           quantity,
           unitPrice);

        _items.Add(item);
        TotalAmount += item.TotalPrice;
    }
    public void MarkInventoryReservationPending()
    {
        Status = OrderStatus.InventoryReservationPending;
    }

    public void MarkInventoryReserved()
    {
        Status = OrderStatus.InventoryReserved;
    }

    public void MarkInventoryReservationFailed()
    {
        Status = OrderStatus.InventoryReservationFailed;
    }
}
