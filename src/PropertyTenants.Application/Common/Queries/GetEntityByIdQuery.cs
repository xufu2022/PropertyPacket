using PropertyTenants.CrossCuttingConcerns.Exceptions;
using PropertyTenants.Domain;
using PropertyTenants.Domain.Repositories;

namespace PropertyTenants.Application.Common.Queries
{
    public class GetEntityByIdQuery<TEntity> : IQuery<TEntity>
        where TEntity : AbstractDomain, IAggregateRoot
    {
        public Guid Id { get; set; }
        public bool ThrowNotFoundIfNull { get; set; }
    }

    internal class GetEntityByIdQueryHandler<TEntity> : IQueryHandler<GetEntityByIdQuery<TEntity>, TEntity>
        where TEntity : AbstractDomain, IAggregateRoot
    {
        private readonly IRepository<TEntity> _repository;

        public GetEntityByIdQueryHandler(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task<TEntity> HandleAsync(GetEntityByIdQuery<TEntity> query, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.FirstOrDefaultAsync(_repository.GetQueryableSet().Where(x => x.Id == query.Id));

            if (query.ThrowNotFoundIfNull && entity == null)
            {
                throw new NotFoundException($"Entity {query.Id} not found.");
            }

            return entity;
        }
    }
}
