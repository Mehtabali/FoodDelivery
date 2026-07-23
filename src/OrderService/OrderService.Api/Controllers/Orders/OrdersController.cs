using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Orders.CreateOrder;

namespace OrderService.Api.Controllers.Orders;

[ApiController]
[Route("api/orders")]
public sealed class OrdersController(CreateOrderHandler createOrderHandler) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CreateOrderResult>> CreateOrder(
        CreateOrderRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateOrderCommand(
            request.CustomerId,
            request.Items
                .Select(item => new CreateOrderItem(item.ProductId, item.Quantity, item.UnitPrice))
                .ToArray());

        var result = await createOrderHandler.HandleAsync(command, cancellationToken);

        return CreatedAtAction(
            nameof(GetOrder),
            new { orderId = result.OrderId },
            result);
    }

    [HttpGet("{orderId:guid}")]
    public IActionResult GetOrder(Guid orderId)
    {
        return Ok(new { orderId });
    }
}
