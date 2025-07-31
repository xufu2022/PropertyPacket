using PropertyTenants.Domain.Entities.Localized;

namespace PropertyTenants.Domain.Repositories;

public interface ISmsMessageRepository : IRepository<SmsMessage>
{
    Task<int> ArchiveMessagesAsync(CancellationToken cancellationToken = default);
}
