using InventoryService.Domain.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryService.Infrastructure.Persistence.Configurations;

internal sealed class InventoryItemConfiguration : IEntityTypeConfiguration<InventoryItem>
{
    public void Configure(EntityTypeBuilder<InventoryItem> builder)
    {
        builder.HasKey(item => item.ProductId);

        builder.Property(item => item.ProductName)
            .HasMaxLength(200)
            .IsRequired();

        builder.HasData(
            new InventoryItem { ProductId = 101, ProductName = "Burger", AvailableQuantity = 20 },
            new InventoryItem { ProductId = 102, ProductName = "Pizza", AvailableQuantity = 15 },
            new InventoryItem { ProductId = 103, ProductName = "Fries", AvailableQuantity = 30 });
    }
}
