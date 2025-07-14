using PropertyTenants.Domain.Common;

namespace PropertyTenants.Domain.Clients
{
    public class User : AbstractDomain
    {
        public required string FriendlyName { get; set; }
        public required string ClientName { get; set; }
        public required string ShortDescription { get; set; }
        public string? Description { get; set; }
        public required string UserName { get; set; }
        public required string PasswordHash { get; set; }
        public required ContactInfo ContactInfo { get; set; }
        public bool IsHost { get; set; }
        public bool IsGuest { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<UserRole> UserRoles { get; set; } = [];
    }
}
