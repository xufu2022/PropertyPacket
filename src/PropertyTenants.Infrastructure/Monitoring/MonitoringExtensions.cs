﻿using Microsoft.Extensions.DependencyInjection;
using PropertyTenants.Infrastructure.Monitoring.AzureApplicationInsights;
using PropertyTenants.Infrastructure.Monitoring.OpenTelemetry;

namespace PropertyTenants.Infrastructure.Monitoring;

public static class MonitoringExtensions
{
    public static IServiceCollection AddMonitoringServices(this IServiceCollection services, MonitoringOptions monitoringOptions = null)
    {
        if (monitoringOptions?.AzureApplicationInsights?.IsEnabled ?? false)
        {
            services.AddAzureApplicationInsights(monitoringOptions.AzureApplicationInsights);
        }

        if (monitoringOptions?.OpenTelemetry?.IsEnabled ?? false)
        {
            services.AddClassifiedAdsOpenTelemetry(monitoringOptions.OpenTelemetry);
        }

        return services;
    }
}
