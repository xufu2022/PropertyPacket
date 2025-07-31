using PropertyTenants.Application.Common.Commands;
using PropertyTenants.Domain.Entities.Clients;
using PropertyTenants.Domain.Repositories;

namespace PropertyTenants.Application.Roles.Commands;

public class DeleteClaimCommand : ICommand
{
    public Role Role { get; set; }
    public RoleClaim Claim { get; set; }
}

internal class DeleteClaimCommandHandler : ICommandHandler<DeleteClaimCommand>
{
    private readonly IRoleRepository _roleRepository;

    public DeleteClaimCommandHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task HandleAsync(DeleteClaimCommand command, CancellationToken cancellationToken = default)
    {
        command.Role.Claims.Remove(command.Claim);
        await _roleRepository.UnitOfWork.SaveChangesAsync();
    }
}
