using System.Diagnostics;
using PropertyTenants.Application.Common.Services;
using PropertyTenants.CrossCuttingConcerns.ExtensionMethods;
using PropertyTenants.Domain.Constants;
using PropertyTenants.Domain.Entities.Clients;
using PropertyTenants.Domain.Entities.Directory;
using PropertyTenants.Domain.Entities.Outbox;
using PropertyTenants.Domain.Events;
using PropertyTenants.Domain.Identity;
using PropertyTenants.Domain.Repositories;

namespace PropertyTenants.Application.FileEntries.EventHandlers;

public class FileEntryUpdatedEventHandler : IDomainEventHandler<EntityUpdatedEvent<FileEntry>>
{
    private readonly ICrudService<AuditLogEntry> _auditSerivce;
    private readonly ICurrentUser _currentUser;
    private readonly IRepository<OutboxEvent> _outboxEventRepository;

    public FileEntryUpdatedEventHandler(ICrudService<AuditLogEntry> auditSerivce,
        ICurrentUser currentUser,
        IRepository<OutboxEvent> outboxEventRepository)
    {
        _auditSerivce = auditSerivce;
        _currentUser = currentUser;
        _outboxEventRepository = outboxEventRepository;
    }

    public async Task HandleAsync(EntityUpdatedEvent<FileEntry> domainEvent, CancellationToken cancellationToken = default)
    {
        await _auditSerivce.AddOrUpdateAsync(new AuditLogEntry(Guid.NewGuid())
        {
            UserId = _currentUser.IsAuthenticated ? _currentUser.UserId : Guid.Empty,
            CreatedDateTime = domainEvent.EventDateTime,
            Action = "UPDATED_FILEENTRY",
            ObjectId = domainEvent.Entity.Id.ToString(),
            Log = domainEvent.Entity.AsJsonString(),
        }, cancellationToken);

        await _outboxEventRepository.AddOrUpdateAsync(new OutboxEvent(Guid.NewGuid())
        {
            EventType = EventTypeConstants.FileEntryUpdated,
            TriggeredById = _currentUser.UserId,
            CreatedDateTime = domainEvent.EventDateTime,
            ObjectId = domainEvent.Entity.Id.ToString(),
            Payload = domainEvent.Entity.AsJsonString(),
            ActivityId = Activity.Current.Id,
        }, cancellationToken);

        await _outboxEventRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}
