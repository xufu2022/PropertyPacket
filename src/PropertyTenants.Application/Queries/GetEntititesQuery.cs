using PropertyTenants.Domain;
using PropertyTenants.Domain.Repositories;

namespace PropertyTenants.Application.Queries
{
    public class GetEntititesQuery<TEntity> : IQuery<List<TEntity>>
        where TEntity : AbstractDomain, IAggregateRoot
    {
    }

    internal class GetEntititesQueryHandler<TEntity>(IRepository<TEntity> repository)
        : IQueryHandler<GetEntititesQuery<TEntity>, List<TEntity>>
        where TEntity : AbstractDomain, IAggregateRoot
    {
        public async Task<List<TEntity>> HandleAsync(GetEntititesQuery<TEntity> query, CancellationToken cancellationToken = default)
        {
            return await repository.ToListAsync(repository.GetQueryableSet());
        }
    }
}
