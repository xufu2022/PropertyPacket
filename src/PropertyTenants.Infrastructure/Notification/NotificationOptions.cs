using PropertyTenants.Infrastructure.Notification.Email;
using PropertyTenants.Infrastructure.Notification.Sms;
using PropertyTenants.Infrastructure.Notification.Web;

namespace PropertyTenants.Infrastructure.Notification;

public class NotificationOptions
{
    public EmailOptions Email { get; set; }

    public SmsOptions Sms { get; set; }

    public WebOptions Web { get; set; }
}
