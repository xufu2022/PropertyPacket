using PropertyTenants.Application.Common.Services;
using PropertyTenants.Domain;

namespace PropertyTenants.Application.Common.Commands
{
    public class DeleteEntityCommand<TEntity> : ICommand
        where TEntity : AbstractDomain, IAggregateRoot
    {
        public TEntity Entity { get; set; }
    }

    internal class DeleteEntityCommandHandler<TEntity> : ICommandHandler<DeleteEntityCommand<TEntity>>
        where TEntity : AbstractDomain, IAggregateRoot
    {
        private readonly ICrudService<TEntity> _crudService;

        public DeleteEntityCommandHandler(ICrudService<TEntity> crudService)
        {
            _crudService = crudService;
        }

        public async Task HandleAsync(DeleteEntityCommand<TEntity> command, CancellationToken cancellationToken = default)
        {
            await _crudService.DeleteAsync(command.Entity);
        }
    }
}
