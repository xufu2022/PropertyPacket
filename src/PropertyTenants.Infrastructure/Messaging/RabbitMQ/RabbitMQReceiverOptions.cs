﻿namespace PropertyTenants.Infrastructure.Messaging.RabbitMQ;

public class RabbitMQReceiverOptions
{
    public string HostName { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    public string QueueName { get; set; }

    public bool AutomaticCreateEnabled { get; set; }

    public string QueueType { get; set; }

    public string ExchangeName { get; set; }

    public string RoutingKey { get; set; }

    public bool SingleActiveConsumer { get; set; }

    public bool MessageEncryptionEnabled { get; set; }

    public string MessageEncryptionKey { get; set; }

    public bool RequeueOnFailure { get; set; }

    public DeadLetterOptions DeadLetter { get; set; }
}

public class DeadLetterOptions
{
    public string ExchangeName { get; set; }

    public string RoutingKey { get; set; }

    public string QueueName { get; set; }

    public bool AutomaticCreateEnabled { get; set; }
}
