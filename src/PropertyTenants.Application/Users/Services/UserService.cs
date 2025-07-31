using PropertyTenants.Application.Common;
using PropertyTenants.Application.Common.Services;
using PropertyTenants.Domain.Entities.Clients;
using PropertyTenants.Domain.Repositories;

namespace PropertyTenants.Application.Users.Services
{
    public class UserService : CrudService<User>, IUserService
    {
        public UserService(IRepository<User> userRepository, Dispatcher dispatcher)
            : base(userRepository, dispatcher)
        {
        }
    }
}
