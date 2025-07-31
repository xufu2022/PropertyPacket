using PropertyTenants.Domain.Entities.Localized;

namespace PropertyTenants.Domain.Repositories;

public interface IEmailMessageRepository : IRepository<EmailMessage>
{
    Task<int> ArchiveMessagesAsync(CancellationToken cancellationToken = default);
}
