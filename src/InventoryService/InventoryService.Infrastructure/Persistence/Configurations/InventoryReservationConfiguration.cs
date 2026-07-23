using InventoryService.Domain.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryService.Infrastructure.Persistence.Configurations;

internal sealed class InventoryReservationConfiguration : IEntityTypeConfiguration<InventoryReservation>
{
    public void Configure(EntityTypeBuilder<InventoryReservation> builder)
    {
        builder.HasKey(reservation => reservation.Id);

        builder.HasIndex(reservation => reservation.CommandMessageId)
            .IsUnique();

        builder.Property(reservation => reservation.Status)
            .HasMaxLength(50)
            .IsRequired();
    }
}
