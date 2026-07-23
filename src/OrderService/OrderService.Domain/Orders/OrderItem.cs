namespace OrderService.Domain.Orders;

public sealed class OrderItem
{
    public Guid Id { get; private set; }

    public Guid OrderId { get; private set; }

    public Guid ProductId { get; private set; }

    public string ProductName { get; private set; } = string.Empty;

    public decimal UnitPrice { get; private set; }

    public int Quantity { get; private set; }

    public decimal TotalPrice => Quantity * UnitPrice;

    private OrderItem()
    {
    }

    internal OrderItem(
        Guid id,
        Guid orderId,
        Guid productId,
        int quantity,
        decimal unitPrice)
    {
        Id = id;
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }


}
