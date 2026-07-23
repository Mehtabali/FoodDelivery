using InventoryService.Infrastructure.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryService.Infrastructure.Persistence.Configurations;

internal sealed class InboxMessageConfiguration : IEntityTypeConfiguration<InboxMessage>
{
    public void Configure(EntityTypeBuilder<InboxMessage> builder)
    {
        builder.HasKey(message => message.MessageId);

        builder.HasIndex(message => message.MessageId)
            .IsUnique();

        builder.Property(message => message.MessageType)
            .HasMaxLength(250)
            .IsRequired();
    }
}
