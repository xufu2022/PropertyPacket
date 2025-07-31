﻿using PropertyTenants.CrossCuttingConcerns.Exceptions;
using PropertyTenants.Domain;
using PropertyTenants.Domain.Events;
using PropertyTenants.Domain.Repositories;

namespace PropertyTenants.Application.Common.Services
{
    public class CrudService<T> : ICrudService<T>
        where T : AbstractDomain, IAggregateRoot
    {
        private readonly IUnitOfWork _unitOfWork;
        protected readonly IRepository<T> _repository;
        protected readonly Dispatcher _dispatcher;

        public CrudService(IRepository<T> repository, Dispatcher dispatcher)
        {
            _unitOfWork = repository.UnitOfWork;
            _repository = repository;
            _dispatcher = dispatcher;
        }

        public Task<List<T>> GetAsync(CancellationToken cancellationToken = default)
        {
            return _repository.ToListAsync(_repository.GetQueryableSet());
        }

        public Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            ValidationException.Requires(id != Guid.Empty, "Invalid Id");
            return _repository.FirstOrDefaultAsync(_repository.GetQueryableSet().Where(x => x.Id == id));
        }

        public async Task AddOrUpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            if (entity.Id.Equals(default))
            {
                await AddAsync(entity, cancellationToken);
            }
            else
            {
                await UpdateAsync(entity, cancellationToken);
            }
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _repository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _dispatcher.DispatchAsync(new EntityCreatedEvent<T>(entity, DateTime.UtcNow), cancellationToken);
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _repository.UpdateAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _dispatcher.DispatchAsync(new EntityUpdatedEvent<T>(entity, DateTime.UtcNow), cancellationToken);
        }

        public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            _repository.Delete(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _dispatcher.DispatchAsync(new EntityDeletedEvent<T>(entity, DateTime.UtcNow), cancellationToken);
        }
    }
}
