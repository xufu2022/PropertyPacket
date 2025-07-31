﻿using PropertyTenants.Domain;

namespace PropertyTenants.Application.Common.Services
{
    public interface ICrudService<T>
        where T : AbstractDomain, IAggregateRoot
    {
        Task<List<T>> GetAsync(CancellationToken cancellationToken = default);

        Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task AddOrUpdateAsync(T entity, CancellationToken cancellationToken = default);

        Task AddAsync(T entity, CancellationToken cancellationToken = default);

        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);

        Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
    }
}
