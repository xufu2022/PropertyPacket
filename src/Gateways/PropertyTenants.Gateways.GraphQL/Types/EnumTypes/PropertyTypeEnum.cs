using PropertyTenants.Domain.Entities.Properties;

namespace PropertyTenants.Gateways.GraphQL.Types.EnumTypes;

public class PropertyTypeEnum : EnumType<PropertyType>
{
    protected override void Configure(IEnumTypeDescriptor<PropertyType> descriptor)
    {
        descriptor
            .Name("PropertyType")
            .Description("The type of property");

        descriptor
            .Value(PropertyType.Apartment)
            .Description("Apartment type property");

        descriptor
            .Value(PropertyType.House)
            .Description("House type property");

        descriptor
            .Value(PropertyType.Hotel)
            .Description("Hotel type property");

        descriptor
            .Value(PropertyType.GuestHome)
            .Description("Guest home type property");

        descriptor
            .Value(PropertyType.UniqueSpace)
            .Description("Unique space type property");

        descriptor
            .Value(PropertyType.Bed)
            .Description("Bed type property");
    }
}
