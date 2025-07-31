﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using PropertyTenants.Infrastructure.Messaging.AzureQueue;
using PropertyTenants.Infrastructure.Messaging.AzureServiceBus;
using PropertyTenants.Infrastructure.Messaging.RabbitMQ;
using PropertyTenants.Infrastructure.Storages.Amazon;
using PropertyTenants.Infrastructure.Storages.Azure;
using PropertyTenants.Infrastructure.Storages.Local;

namespace PropertyTenants.Infrastructure.HealthChecks;

public static class HealthCheckBuilderExtensions
{
    public static IHealthChecksBuilder AddHttp(
        this IHealthChecksBuilder builder,
        string uri,
        string name = default,
        HealthStatus? failureStatus = default,
        IEnumerable<string> tags = default,
        TimeSpan? timeout = default)
    {
        return builder.Add(new HealthCheckRegistration(
            name,
            new HttpHealthCheck(uri),
            failureStatus,
            tags,
            timeout));
    }

    public static IHealthChecksBuilder AddSqlServer(
        this IHealthChecksBuilder builder,
        string connectionString,
        string healthQuery = default,
        string name = default,
        HealthStatus? failureStatus = default,
        IEnumerable<string> tags = default,
        TimeSpan? timeout = default)
    {
        return builder.Add(new HealthCheckRegistration(
            name,
            new SqlServerHealthCheck(connectionString, healthQuery),
            failureStatus,
            tags,
            timeout));
    }

    public static IHealthChecksBuilder AddAmazonS3(
        this IHealthChecksBuilder builder,
        AmazonOptions amazonOptions,
        string name = default,
        HealthStatus? failureStatus = default,
        IEnumerable<string> tags = default,
        TimeSpan? timeout = default)
    {
        return builder.Add(new HealthCheckRegistration(
            name,
            new AmazonS3HealthCheck(amazonOptions),
            failureStatus,
            tags,
            timeout));
    }

    public static IHealthChecksBuilder AddAzureBlobStorage(
        this IHealthChecksBuilder builder,
        AzureBlobOption azureBlobOptions,
        string name = default,
        HealthStatus? failureStatus = default,
        IEnumerable<string> tags = default,
        TimeSpan? timeout = default)
    {
        return builder.Add(new HealthCheckRegistration(
            name,
            new AzureBlobStorageHealthCheck(azureBlobOptions),
            failureStatus,
            tags,
            timeout));
    }

    public static IHealthChecksBuilder AddAzureQueueStorage(
        this IHealthChecksBuilder builder,
        string connectionString,
        string queueName,
        string name = default,
        HealthStatus? failureStatus = default,
        IEnumerable<string> tags = default,
        TimeSpan? timeout = default)
    {
        return builder.Add(new HealthCheckRegistration(
            name,
            new AzureQueueStorageHealthCheck(
                connectionString: connectionString,
                queueName: queueName),
            failureStatus,
            tags,
            timeout));
    }

    public static IHealthChecksBuilder AddAzureServiceBusQueue(
        this IHealthChecksBuilder builder,
        string connectionString,
        string queueName,
        string name = default,
        HealthStatus? failureStatus = default,
        IEnumerable<string> tags = default,
        TimeSpan? timeout = default)
    {
        return builder.Add(new HealthCheckRegistration(
            name,
            new AzureServiceBusQueueHealthCheck(
                connectionString: connectionString,
                queueName: queueName),
            failureStatus,
            tags,
            timeout));
    }

    //public static IHealthChecksBuilder AddKafka(
    //    this IHealthChecksBuilder builder,
    //    string bootstrapServers,
    //    string topic,
    //    string name = default,
    //    HealthStatus? failureStatus = default,
    //    IEnumerable<string> tags = default,
    //    TimeSpan? timeout = default)
    //{
    //    return builder.Add(new HealthCheckRegistration(
    //        name,
    //        new KafkaHealthCheck(bootstrapServers: bootstrapServers, topic: topic),
    //        failureStatus,
    //        tags,
    //        timeout));
    //}

    public static IHealthChecksBuilder AddRabbitMQ(
        this IHealthChecksBuilder builder,
        RabbitMQHealthCheckOptions options,
        string name = default,
        HealthStatus? failureStatus = default,
        IEnumerable<string> tags = default,
        TimeSpan? timeout = default)
    {
        return builder.Add(new HealthCheckRegistration(
            name,
            new RabbitMQHealthCheck(options),
            failureStatus,
            tags,
            timeout));
    }

    public static IHealthChecksBuilder AddLocalFile(
        this IHealthChecksBuilder builder,
        LocalFileHealthCheckOptions options,
        string name = default,
        HealthStatus? failureStatus = default,
        IEnumerable<string> tags = default,
        TimeSpan? timeout = default)
    {
        return builder.Add(new HealthCheckRegistration(
            name,
            new LocalFileHealthCheck(options),
            failureStatus,
            tags,
            timeout));
    }
}