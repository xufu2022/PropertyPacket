using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace PropertyTenants.Infrastructure.HealthChecks;

public class HttpHealthCheck(string uri) : IHealthCheck
{
    private static readonly HttpClient HttpClient = new HttpClient();

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await HttpClient.GetAsync(uri, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return HealthCheckResult.Healthy($"Uri: '{uri}', StatusCode: '{response.StatusCode}'");
            }
            else
            {
                return new HealthCheckResult(context.Registration.FailureStatus, $"Uri: '{uri}', StatusCode: '{response.StatusCode}'");
            }
        }
        catch (Exception exception)
        {
            return new HealthCheckResult(context.Registration.FailureStatus, $"Uri: '{uri}', Exception: '{exception.Message}'", exception);
        }
    }
}
