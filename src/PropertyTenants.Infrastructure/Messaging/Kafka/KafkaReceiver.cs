﻿using System.Text.Json;
using PropertyTenants.Domain.Infrastructure.Messaging;

namespace PropertyTenants.Infrastructure.Messaging.Kafka;

public class KafkaReceiver<TConsumer, T> : IMessageReceiver<TConsumer, T>, IDisposable
{
    private readonly IConsumer<Ignore, string> _consumer;

    public KafkaReceiver(string bootstrapServers, string topic, string groupId)
    {
        var config = new ConsumerConfig
        {
            GroupId = groupId,
            BootstrapServers = bootstrapServers,
            AutoOffsetReset = AutoOffsetReset.Earliest,
        };

        _consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        _consumer.Subscribe(topic);
    }

    public void Dispose()
    {
        _consumer.Dispose();
    }

    public async Task ReceiveAsync(Func<T, MetaData, Task> action, CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                var consumeResult = _consumer.Consume(cancellationToken);

                if (consumeResult.IsPartitionEOF)
                {
                    continue;
                }

                var message = JsonSerializer.Deserialize<Message<T>>(consumeResult.Message.Value);
                await action(message.Data, message.MetaData);
            }
            catch (ConsumeException e)
            {
                Console.WriteLine($"Consume error: {e.Error.Reason}");
            }
        }
    }
}
