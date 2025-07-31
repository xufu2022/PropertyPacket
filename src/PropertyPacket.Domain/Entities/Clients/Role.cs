namespace PropertyTenants.Domain.Entities.Clients
{
    public class Role(Guid id) : AbstractDomain(id), IAggregateRoot
    {
        public required string Name { get; set; }
        public required string Description { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; } = [];

        public IList<RoleClaim> Claims { get; set; }
    }

    public class UserRole(Guid id) : AbstractDomain(id)
    {
        public required Guid UserId { get; set; }
        public required Guid RoleId { get; set; }
        public User? User { get; set; }
        public Role? Role { get; set; } 
    }

    public class RoleClaim(Guid id) : AbstractDomain(id)
    {
        public string Type { get; set; }
        public string Value { get; set; }

        public Role Role { get; set; }
    }
}
