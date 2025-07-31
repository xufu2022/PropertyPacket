namespace PropertyTenants.Domain.Infrastructure.Messaging;

public interface IMessageReceiver<TConsumer, T>
{
    Task ReceiveAsync(Func<T, MetaData, Task> action, CancellationToken cancellationToken);
}
