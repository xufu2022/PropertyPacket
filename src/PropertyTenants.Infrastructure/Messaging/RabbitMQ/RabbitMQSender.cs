﻿using System.Security.Cryptography;
using CryptographyHelper;
using CryptographyHelper.SymmetricAlgorithms;
using PropertyTenants.Domain.Infrastructure.Messaging;
using RabbitMQ.Client;

namespace PropertyTenants.Infrastructure.Messaging.RabbitMQ;

public class RabbitMQSender<T> : IMessageSender<T>
{
    private readonly RabbitMQSenderOptions _options;
    private readonly ConnectionFactory _connectionFactory;
    private readonly string _exchangeName;
    private readonly string _routingKey;

    public RabbitMQSender(RabbitMQSenderOptions options)
    {
        _options = options;

        _connectionFactory = new ConnectionFactory
        {
            HostName = options.HostName,
            UserName = options.UserName,
            Password = options.Password,
        };

        _exchangeName = options.ExchangeName;
        _routingKey = options.RoutingKey;
    }

    public async Task SendAsync(T message, MetaData metaData = null, CancellationToken cancellationToken = default)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync(cancellationToken);
        using var channel = await connection.CreateChannelAsync(cancellationToken: cancellationToken);
        var body = new Message<T>
        {
            Data = message,
            MetaData = metaData,
        }.GetBytes();

        if (_options.MessageEncryptionEnabled)
        {
            var iv = SymmetricCrypto.GenerateKey(16);
            body = (iv.ToBase64String() + "." + body.UseAES(_options.MessageEncryptionKey.FromBase64String())
            .WithCipher(CipherMode.CBC)
            .WithIV(iv)
            .WithPadding(PaddingMode.PKCS7)
            .Encrypt().ToBase64String()).GetBytes();
        }

        var properties = new BasicProperties
        {
            Persistent = true
        };

        await channel.BasicPublishAsync(exchange: _exchangeName,
                               routingKey: _routingKey,
                               mandatory: true,
                               basicProperties: properties,
                               body: body,
                               cancellationToken: cancellationToken);
    }
}
