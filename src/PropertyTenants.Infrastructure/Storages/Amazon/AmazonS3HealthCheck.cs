using System.Text;
using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace PropertyTenants.Infrastructure.Storages.Amazon;

public class AmazonS3HealthCheck(AmazonOptions options) : IHealthCheck
{
    private readonly IAmazonS3 _client = options.CreateAmazonS3Client();

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            var fileName = options.Path + $"HealthCheck/{DateTime.Now:yyyy-MM-dd-hh-mm-ss}-{Guid.NewGuid()}.txt";
            var fileTransferUtility = new TransferUtility(_client);

            using var stream = new MemoryStream(Encoding.UTF8.GetBytes($"HealthCheck {DateTime.Now}"));
            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = stream,
                Key = fileName,
                BucketName = options.BucketName,
                CannedACL = S3CannedACL.NoACL,
            };

            await fileTransferUtility.UploadAsync(uploadRequest, cancellationToken);
            await _client.DeleteObjectAsync(options.BucketName, fileName, cancellationToken);

            return HealthCheckResult.Healthy($"BucketName: {options.BucketName}");
        }
        catch (Exception exception)
        {
            return new HealthCheckResult(context.Registration.FailureStatus, null, exception);
        }
    }
}
