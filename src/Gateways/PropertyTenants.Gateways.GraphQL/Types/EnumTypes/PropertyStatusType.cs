using PropertyTenants.Domain.Entities.Properties;

namespace PropertyTenants.Gateways.GraphQL.Types.EnumTypes;

public class PropertyStatusType : EnumType<PropertyStatus>
{
    protected override void Configure(IEnumTypeDescriptor<PropertyStatus> descriptor)
    {
        descriptor
            .Name("PropertyStatus")
            .Description("The status of a property listing");

        descriptor
            .Value(PropertyStatus.Available)
            .Description("Property is available for booking");

        descriptor
            .Value(PropertyStatus.Unavailable)
            .Description("Property is not available for booking");

        descriptor
            .Value(PropertyStatus.Maintenance)
            .Description("Property is under maintenance");

        descriptor
            .Value(PropertyStatus.Archived)
            .Description("Property has been archived");
    }
}
