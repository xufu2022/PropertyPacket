using PropertyTenants.Infrastructure.Monitoring.AzureApplicationInsights;
using PropertyTenants.Infrastructure.Monitoring.OpenTelemetry;

namespace PropertyTenants.Infrastructure.Monitoring;

public class MonitoringOptions
{
    public AzureApplicationInsightsOptions AzureApplicationInsights { get; set; }

    public OpenTelemetryOptions OpenTelemetry { get; set; }
}
