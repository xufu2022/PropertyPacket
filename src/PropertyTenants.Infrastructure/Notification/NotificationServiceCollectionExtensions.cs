using Microsoft.Extensions.DependencyInjection;
using PropertyTenants.Infrastructure.Notification.Email;
using PropertyTenants.Infrastructure.Notification.Sms;

namespace PropertyTenants.Infrastructure.Notification;

public static class NotificationServiceCollectionExtensions
{
    public static IServiceCollection AddNotificationServices(this IServiceCollection services, NotificationOptions options)
    {
        services.AddEmailNotification(options.Email);

        services.AddSmsNotification(options.Sms);

        return services;
    }
}
