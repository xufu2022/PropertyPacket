namespace PropertyPacket.Domain.Sites
{
    public abstract class AbstractDomain
    {
        public Guid Id { get; set; }

    }

    public class Site : AbstractDomain
    {
        public required string Name { get; set; }
        public required string Url { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string Keywords { get; set; }
        public bool Published { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public SiteAddress? Address { get; set; }
        public int AddressId { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }

    }

    public class SiteInfo : AbstractDomain
    {
        private Guid SiteId { get; set; }
        public required string ImageUrl { get; set; }
        public required string Title { get; set; }
        public required string ContactInfo { get; set; }
        public required string Copyright { get; set; }

    }

    public record SiteAddress(string Line1, string Line2, string City, string Country, string PostCode){}

    public record Faq(Guid Id, string Question, string Answer)
    {
    }
}

