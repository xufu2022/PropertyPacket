using PropertyTenants.Domain.Notification;

namespace PropertyTenants.Infrastructure.Notification.Sms.Fake;

public class FakeSmsNotification : ISmsNotification
{
    public Task SendAsync(ISmsMessage smsMessage, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
