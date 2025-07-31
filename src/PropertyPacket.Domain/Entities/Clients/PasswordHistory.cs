namespace PropertyTenants.Domain.Entities.Clients
{
    public class PasswordHistory(Guid id) : AbstractDomain(id)
    {
        public Guid UserId { get; set; }

        public string PasswordHash { get; set; }

        public virtual User User { get; set; }
    }
}
