namespace PropertyPacket.Domain.Store
{
    public class Store : BaseEntity
    {
        public required string Name { get; set; }
        public StoreInfo? StoreInfo { get; set; }
    }
}
