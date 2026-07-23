using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Orders;

namespace OrderService.Application.Abstractions.Persistence;

public interface IOrderDbContext
{
    DbSet<Order> Orders { get; }

    Task AddOutboxMessageAsync(
        Guid id,
        Guid correlationId,
        string type,
        string content,
        DateTime occurredOnUtc,
        CancellationToken cancellationToken);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
