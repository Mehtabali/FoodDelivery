namespace OrderService.Api.Controllers.Orders;

public sealed record CreateOrderItemRequest(
    Guid ProductId,
    int Quantity,
    decimal UnitPrice);
