using PropertyTenants.Domain.Notification;

namespace PropertyTenants.Infrastructure.Notification.Email.Fake;

public class FakeEmailNotification : IEmailNotification
{
    public Task SendAsync(IEmailMessage emailMessage, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
