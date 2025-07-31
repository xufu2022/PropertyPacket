using Microsoft.Extensions.DependencyInjection;
using PropertyTenants.Domain.Notification;
using PropertyTenants.Infrastructure.Notification.Web.Fake;
using PropertyTenants.Infrastructure.Notification.Web.SignalR;

namespace PropertyTenants.Infrastructure.Notification.Web;

public static class WebNotificationServiceCollectionExtensions
{
    public static IServiceCollection AddSignalRWebNotification<T>(this IServiceCollection services, SignalROptions options)
    {
        services.AddSingleton<IWebNotification<T>>(new SignalRNotification<T>(options.Endpoint, options.Hubs[typeof(T).Name], options.MethodNames[typeof(T).Name]));
        return services;
    }

    public static IServiceCollection AddFakeWebNotification<T>(this IServiceCollection services)
    {
        services.AddSingleton<IWebNotification<T>>(new FakeWebNotification<T>());
        return services;
    }

    public static IServiceCollection AddWebNotification<T>(this IServiceCollection services, WebOptions options)
    {
        if (options.UsedFake())
        {
            services.AddFakeWebNotification<T>();
        }
        else if (options.UsedSignalR())
        {
            services.AddSignalRWebNotification<T>(options.SignalR);
        }

        return services;
    }
}
