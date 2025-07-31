using PropertyTenants.Application.Common.Commands;
using PropertyTenants.Domain.IdentityProviders;
using PropertyTenants.Domain.Repositories;

namespace PropertyTenants.Application.Users.Commands
{
    public class SyncUsersCommand : ICommand
    {
        public int SyncedUsersCount { get; set; }
    }

    public class SyncUsersCommandHandler : ICommandHandler<SyncUsersCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IServiceProvider _serviceProvider;

        public SyncUsersCommandHandler(IUserRepository userRepository,
            IServiceProvider serviceProvider)
        {
            _userRepository = userRepository;
            _serviceProvider = serviceProvider;
        }

        public async Task HandleAsync(SyncUsersCommand command, CancellationToken cancellationToken = default)
        {
            //await SyncToAuth0(command);

            await SyncToAzureAdB2C(command);
        }

        private async Task SyncToAzureAdB2C(SyncUsersCommand command)
        {
            var provider = (IAzureActiveDirectoryB2CIdentityProvider)_serviceProvider.GetService(typeof(IAzureActiveDirectoryB2CIdentityProvider));

            if (provider is null)
            {
                return;
            }

            var users = _userRepository.GetQueryableSet()
                .Where(x => x.AzureAdB2CUserId == null)
                .Take(50)
                .ToList();

            foreach (var user in users)
            {
                var existingUser = await provider.GetUserByUsernameAsync(user.UserName);

                if (existingUser != null)
                {
                    user.AzureAdB2CUserId = existingUser.Id;
                }
                else
                {
                    var newUser = new Domain.IdentityProviders.User
                    {
                        Username = user.UserName,
                        Email = user.Email,
                        Password = Guid.NewGuid().ToString(),
                        FirstName = "FirstName",
                        LastName = "LastName"
                    };

                    await provider.CreateUserAsync(newUser);

                    user.AzureAdB2CUserId = newUser.Id;
                }

                await _userRepository.UnitOfWork.SaveChangesAsync();
            }

            command.SyncedUsersCount += users.Count;
        }

    }
}
