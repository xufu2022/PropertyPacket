namespace PropertyTenants.Gateways.GraphQL.Types.InputTypes
{
    public class AddFeatureInput
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public int FeatureGroupId { get; set; }
    }

    public class UpdateFeatureInput
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public int FeatureGroupId { get; set; }
    }
}
