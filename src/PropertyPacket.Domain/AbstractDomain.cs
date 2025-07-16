namespace PropertyTenants.Domain
{
    public abstract class AbstractDomain
    {
        private AbstractDomain()
        {
        }

        protected AbstractDomain(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; init; }
    }
}
