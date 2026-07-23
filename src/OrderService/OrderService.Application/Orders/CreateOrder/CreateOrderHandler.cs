using System.Text.Json;
using Messaging.Contracts.Commands;
using OrderService.Application.Abstractions.Persistence;
using OrderService.Domain.Orders;

namespace OrderService.Application.Orders.CreateOrder;

public sealed class CreateOrderHandler(IOrderDbContext dbContext)
{
    public async Task<CreateOrderResult> HandleAsync(
        CreateOrderCommand command,
        CancellationToken cancellationToken)
    {
        if (command.CustomerId == Guid.Empty)
        {
            throw new ArgumentException("CustomerId is required.", nameof(command));
        }

        if (command.Items is null || command.Items.Count == 0)
        {
            throw new ArgumentException("At least one order item is required.", nameof(command));
        }

        var order = new Order(command.CustomerId);

        foreach (var item in command.Items)
        {
            order.Add(item.ProductId, item.Quantity, item.UnitPrice);
        }

        order.MarkInventoryReservationPending();

        var messageId = Guid.NewGuid();
        var reserveInventoryCommand = new ReserveInventoryCommand
        {
            MessageId = messageId,
            CorrelationId = order.Id,
            OccurredOnUtc = DateTime.UtcNow,
            OrderId = order.Id,
            Items = command.Items
                .Select(item => new ReserveInventoryItem(item.ProductId, item.Quantity))
                .ToArray()
        };

        var messageContent = JsonSerializer.Serialize(reserveInventoryCommand);

        await dbContext.Orders.AddAsync(order, cancellationToken);
        await dbContext.AddOutboxMessageAsync(
            messageId,
            reserveInventoryCommand.CorrelationId,
            nameof(ReserveInventoryCommand),
            messageContent,
            reserveInventoryCommand.OccurredOnUtc,
            cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateOrderResult(order.Id, order.Status.ToString());
    }
}
