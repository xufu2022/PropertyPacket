using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyTenants.Domain.Clients
{
    public class Role(Guid id) : AbstractDomain(id)
    {
        public required string Name { get; set; }
        public required string Description { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; } = [];
    }

    public class UserRole(Guid id) : AbstractDomain(id)
    {
        public required Guid UserId { get; set; }
        public required Guid RoleId { get; set; }
        public User? User { get; set; }
        public Role? Role { get; set; } 
    }
}
