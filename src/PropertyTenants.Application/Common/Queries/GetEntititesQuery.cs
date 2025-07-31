using PropertyTenants.Domain;
using PropertyTenants.Domain.Repositories;

namespace PropertyTenants.Application.Common.Queries
{
    public class GetEntititesQuery<TEntity> : IQuery<List<TEntity>>
        where TEntity : AbstractDomain, IAggregateRoot
    {
    }

    internal class GetEntititesQueryHandler<TEntity> : IQueryHandler<GetEntititesQuery<TEntity>, List<TEntity>>
        where TEntity : AbstractDomain, IAggregateRoot
    {
        private readonly IRepository<TEntity> _repository;

        public GetEntititesQueryHandler(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task<List<TEntity>> HandleAsync(GetEntititesQuery<TEntity> query, CancellationToken cancellationToken = default)
        {
            return await _repository.ToListAsync(_repository.GetQueryableSet());
        }
    }
}
