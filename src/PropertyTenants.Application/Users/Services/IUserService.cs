using PropertyTenants.Application.Common.Services;
using PropertyTenants.Domain.Entities.Clients;

namespace PropertyTenants.Application.Users.Services
{
    public interface IUserService : ICrudService<User>
    {
    }
}
