namespace PropertyTenants.Domain.Entities.Clients
{
    public class UserToken(Guid id) : AbstractDomain(id)
    {
        public Guid UserId { get; set; }

        public string LoginProvider { get; set; }

        public string TokenName { get; set; }

        public string TokenValue { get; set; }
    }
}
