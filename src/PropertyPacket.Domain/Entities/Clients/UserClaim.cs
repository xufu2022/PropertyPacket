using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyTenants.Domain.Entities.Clients
{
    public class UserClaim(Guid id) : AbstractDomain(id)
    {
        public string Type { get; set; }
        public string Value { get; set; }

        public User User { get; set; }
    }
}
