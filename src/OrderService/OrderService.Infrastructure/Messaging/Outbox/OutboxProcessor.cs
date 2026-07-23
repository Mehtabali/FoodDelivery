using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OrderService.Application.Abstractions.Messaging;
using OrderService.Infrastructure.Persistence;

namespace OrderService.Infrastructure.Messaging.Outbox;

internal sealed class OutboxProcessor(
    IServiceScopeFactory serviceScopeFactory,
    ILogger<OutboxProcessor> logger) : BackgroundService
{
    private const int BatchSize = 20;
    private const int MaximumRetryCount = 5;
    private static readonly TimeSpan PollInterval = TimeSpan.FromSeconds(5);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await ProcessOutboxMessagesAsync(stoppingToken);
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Unexpected error while processing order outbox messages.");
            }

            await Task.Delay(PollInterval, stoppingToken);
        }
    }

    private async Task ProcessOutboxMessagesAsync(CancellationToken cancellationToken)
    {
        await using var scope = serviceScopeFactory.CreateAsyncScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
        var messagePublisher = scope.ServiceProvider.GetRequiredService<IMessagePublisher>();

        var messages = await dbContext.OutboxMessages
            .Where(message => message.ProcessedOnUtc == null)
            .Where(message => message.RetryCount < MaximumRetryCount)
            .OrderBy(message => message.OccurredOnUtc)
            .Take(BatchSize)
            .ToListAsync(cancellationToken);

        foreach (var message in messages)
        {
            try
            {
                await messagePublisher.PublishAsync(
                    message.Id,
                    message.CorrelationId,
                    message.Type,
                    message.Content,
                    cancellationToken);

                message.MarkAsProcessed();

                logger.LogInformation(
                    "Published outbox message {MessageId} of type {MessageType}.",
                    message.Id,
                    message.Type);
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                throw;
            }
            catch (Exception exception)
            {
                message.MarkAsFailed(TruncateError(exception.ToString()));

                logger.LogError(
                    exception,
                    "Failed to publish outbox message {MessageId} of type {MessageType}.",
                    message.Id,
                    message.Type);
            }

            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    private static string TruncateError(string error)
    {
        return error.Length <= 4000 ? error : error[..4000];
    }
}
