using PropertyTenants.CrossCuttingConcerns.Exceptions;
using PropertyTenants.Domain;
using PropertyTenants.Domain.Repositories;

namespace PropertyTenants.Application.Queries
{
    public class GetEntityByIdQuery<TEntity> : IQuery<TEntity>
        where TEntity : AbstractDomain, IAggregateRoot
    {
        public Guid Id { get; set; }
        public bool ThrowNotFoundIfNull { get; set; }
    }

    internal class GetEntityByIdQueryHandler<TEntity>(IRepository<TEntity> repository)
        : IQueryHandler<GetEntityByIdQuery<TEntity>, TEntity>
        where TEntity : AbstractDomain, IAggregateRoot
    {
        public async Task<TEntity> HandleAsync(GetEntityByIdQuery<TEntity> query, CancellationToken cancellationToken = default)
        {
            var entity = await repository.FirstOrDefaultAsync(repository.GetQueryableSet().Where(x => x.Id == query.Id));

            if (query.ThrowNotFoundIfNull && entity == null)
            {
                throw new NotFoundException($"Entity {query.Id} not found.");
            }

            return entity;
        }
    }
}
