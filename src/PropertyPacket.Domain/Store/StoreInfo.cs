namespace PropertyTenants.Domain.Store
{
    public class StoreInfo : BaseEntity
    {
        public int StoreId { get; set; }
        public required string Description { get; set; }

        public required Address AddressInfo { get; set; }
    }

    public readonly record struct Address(string Line1, string? Line2, string City, string Country, string PostCode);
}
