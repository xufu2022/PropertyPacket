﻿using PropertyTenants.Application.Common.Services;
using PropertyTenants.Domain;

namespace PropertyTenants.Application.Common.Commands
{
    public class AddEntityCommand<TEntity> : ICommand
        where TEntity : AbstractDomain, IAggregateRoot
    {
        public AddEntityCommand(TEntity entity)
        {
            Entity = entity;
        }

        public TEntity Entity { get; set; }
    }

    internal class AddEntityCommandHandler<TEntity> : ICommandHandler<AddEntityCommand<TEntity>>
        where TEntity : AbstractDomain, IAggregateRoot
    {
        private readonly ICrudService<TEntity> _crudService;

        public AddEntityCommandHandler(ICrudService<TEntity> crudService)
        {
            _crudService = crudService;
        }

        public async Task HandleAsync(AddEntityCommand<TEntity> command, CancellationToken cancellationToken = default)
        {
            await _crudService.AddAsync(command.Entity);
        }
    }
}
