using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PropertyTenants.Domain.Infrastructure.Messaging;

namespace PropertyTenants.Infrastructure.HostedServices;

public sealed class MessageBusConsumerBackgroundService<TConsumer, T>(
    ILogger<MessageBusConsumerBackgroundService<TConsumer, T>> logger,
    IMessageBus messageBus)
    : BackgroundService
    where T : IMessageBusEvent
{
    private readonly ILogger<MessageBusConsumerBackgroundService<TConsumer, T>> _logger = logger;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await messageBus.ReceiveAsync<TConsumer, T>(stoppingToken);
    }
}
