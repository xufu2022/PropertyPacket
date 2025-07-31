namespace PropertyTenants.Domain.Events;

public class EntityDeletedEvent<T>(T entity, DateTime eventDateTime) : IDomainEvent
    where T : AbstractDomain
{

    public T Entity { get; } = entity;

    public DateTime EventDateTime { get; } = eventDateTime;
}
