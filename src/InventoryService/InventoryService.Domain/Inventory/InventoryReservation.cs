namespace InventoryService.Domain.Inventory;

public sealed class InventoryReservation
{
    public Guid Id { get; set; }

    public Guid OrderId { get; set; }

    public Guid CommandMessageId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public string Status { get; set; } = string.Empty;

    public DateTime CreatedAtUtc { get; set; }
}
