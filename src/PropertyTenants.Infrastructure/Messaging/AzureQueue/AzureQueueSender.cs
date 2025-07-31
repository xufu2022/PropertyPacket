﻿using System.Text.Json;
using Azure.Storage.Queues;
using PropertyTenants.Domain.Infrastructure.Messaging;

namespace PropertyTenants.Infrastructure.Messaging.AzureQueue;

public class AzureQueueSender<T> : IMessageSender<T>
{
    private readonly string _connectionString;
    private readonly string _queueName;

    public AzureQueueSender(string connectionString, string queueName)
    {
        _connectionString = connectionString;
        _queueName = queueName;
    }

    public async Task SendAsync(T message, MetaData metaData, CancellationToken cancellationToken = default)
    {
        var queueClient = new QueueClient(_connectionString, _queueName);
        await queueClient.CreateIfNotExistsAsync(cancellationToken: cancellationToken);

        var jsonMessage = JsonSerializer.Serialize(new Message<T>
        {
            Data = message,
            MetaData = metaData,
        });

        await queueClient.SendMessageAsync(jsonMessage, cancellationToken);
    }
}
