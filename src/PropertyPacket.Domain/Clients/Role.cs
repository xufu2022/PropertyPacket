using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyTenants.Domain.Clients
{
    public class Role : AbstractDomain
    {
        public required string Name { get; set; }
        public required string Description { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; } = [];
    }

    public class UserRole : AbstractDomain
    {
        public required Guid UserId { get; set; }
        public required Guid RoleId { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual Role Role { get; set; } = null!;
    }
}
