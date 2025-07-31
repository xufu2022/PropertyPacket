﻿using System.Text;
using System.Text.Json;
using Azure.Messaging.ServiceBus;
using PropertyTenants.Domain.Infrastructure.Messaging;

namespace PropertyTenants.Infrastructure.Messaging.AzureServiceBus;

public class AzureServiceBusSubscriptionReceiver<TConsumer, T> : IMessageReceiver<TConsumer, T>
{
    private readonly string _connectionString;
    private readonly string _topicName;
    private readonly string _subscriptionName;

    public AzureServiceBusSubscriptionReceiver(string connectionString, string topicName, string subscriptionName)
    {
        _connectionString = connectionString;
        _topicName = topicName;
        _subscriptionName = subscriptionName;
    }

    public async Task ReceiveAsync(Func<T, MetaData, Task> action, CancellationToken cancellationToken)
    {
        await ReceiveStringAsync(async retrievedMessage =>
        {
            var message = JsonSerializer.Deserialize<Message<T>>(retrievedMessage);
            await action(message.Data, message.MetaData);
        }, cancellationToken);
    }

    private async Task ReceiveStringAsync(Func<string, Task> action, CancellationToken cancellationToken)
    {
        await using var client = new ServiceBusClient(_connectionString);
        ServiceBusReceiver receiver = client.CreateReceiver(_topicName, _subscriptionName);

        while (!cancellationToken.IsCancellationRequested)
        {
            var retrievedMessage = await receiver.ReceiveMessageAsync(cancellationToken: cancellationToken);

            if (retrievedMessage != null)
            {
                await action(Encoding.UTF8.GetString(retrievedMessage.Body));
                await receiver.CompleteMessageAsync(retrievedMessage, cancellationToken);
            }
            else
            {
                await Task.Delay(1000, cancellationToken);
            }
        }
    }
}
