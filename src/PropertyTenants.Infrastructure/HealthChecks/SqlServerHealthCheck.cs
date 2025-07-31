using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace PropertyTenants.Infrastructure.HealthChecks;

public class SqlServerHealthCheck(string connectionString, string sql) : IHealthCheck
{
    private readonly string _connectionString = connectionString ?? throw new ArgumentNullException("connectionString");

    private readonly string _sql = sql ?? throw new ArgumentNullException("sql");

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            await using SqlConnection connection = new SqlConnection(_connectionString);
            await connection.OpenAsync(cancellationToken);
            await using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = _sql;
                await command.ExecuteScalarAsync(cancellationToken);
            }

            return HealthCheckResult.Healthy();
        }
        catch (Exception exception)
        {
            return new HealthCheckResult(context.Registration.FailureStatus, null, exception);
        }
    }
}
