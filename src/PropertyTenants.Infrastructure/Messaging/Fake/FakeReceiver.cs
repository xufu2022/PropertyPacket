using PropertyTenants.Domain.Infrastructure.Messaging;

namespace PropertyTenants.Infrastructure.Messaging.Fake;

public class FakeReceiver<TConsumer, T> : IMessageReceiver<TConsumer, T>
{
    public Task ReceiveAsync(Func<T, MetaData, Task> action, CancellationToken cancellationToken)
    {
        // do nothing
        return Task.CompletedTask;
    }
}
