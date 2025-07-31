using PropertyTenants.Domain.Entities.Common;
using PropertyTenants.Domain.IdentityProviders;

namespace PropertyTenants.Domain.Entities.Clients
{
    public class User(Guid id) : AbstractDomain(id), IAggregateRoot
    {
        public required string FriendlyName { get; set; }
        public required string ClientName { get; set; }
        public required string ShortDescription { get; set; }
        public string? Description { get; set; }
        public required string UserName { get; set; }
        public required string PasswordHash { get; set; }
        public required ContactInfo ContactInfo { get; set; }
        public Address? Address { get; init; }
        public int AddressId { get; init; }
        public bool IsHost { get; set; }
        public bool IsGuest { get; set; }

        public string Email { get; set; } = "";
        public int AccessFailedCount { get; set; }

        public string AzureAdB2CUserId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<UserRole> UserRoles { get; set; } = [];

        public IList<UserClaim> Claims { get; set; }
    }
}
