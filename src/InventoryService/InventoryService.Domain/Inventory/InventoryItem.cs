namespace InventoryService.Domain.Inventory;

public sealed class InventoryItem
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public int AvailableQuantity { get; set; }
}
