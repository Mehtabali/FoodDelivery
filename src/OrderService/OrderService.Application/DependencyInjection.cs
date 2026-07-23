using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Orders.CreateOrder;

namespace OrderService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateOrderHandler>();

        return services;
    }
}
