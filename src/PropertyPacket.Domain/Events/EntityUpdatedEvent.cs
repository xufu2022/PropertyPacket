namespace PropertyTenants.Domain.Events;

public class EntityUpdatedEvent<T>(T entity, DateTime eventDateTime) : IDomainEvent
    where T : AbstractDomain
{
    public T Entity { get; } = entity;

    public DateTime EventDateTime { get; } = eventDateTime;
}
