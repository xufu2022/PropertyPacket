using PropertyTenants.Domain.Entities.Properties;

namespace PropertyTenants.Gateways.GraphQL.Types.ObjectTypes;

[ObjectType]
public class PropertyDetailType : ObjectType<PropertyDetail>
{
    protected override void Configure(IObjectTypeDescriptor<PropertyDetail> descriptor)
    {
        descriptor
            .Name("PropertyDetail")
            .Description("Detailed information about a property");

        descriptor
            .Field(f => f.Id)
            .Description("The unique identifier")
            .Type<NonNullType<UuidType>>();

        descriptor
            .Field(f => f.PropertyId)
            .Description("The property ID")
            .Type<NonNullType<UuidType>>();

        descriptor
            .Field(f => f.Bedrooms)
            .Description("Number of bedrooms")
            .Type<IntType>();

        descriptor
            .Field(f => f.Bathrooms)
            .Description("Number of bathrooms")
            .Type<IntType>();

        descriptor
            .Field(f => f.MaxGuests)
            .Description("Maximum number of guests")
            .Type<IntType>();

        descriptor
            .Field(f => f.Area)
            .Description("Area in square meters")
            .Type<DecimalType>();

        descriptor
            .Field(f => f.Description)
            .Description("Detailed description")
            .Type<StringType>();

        descriptor
            .Field(f => f.Rules)
            .Description("Property rules")
            .Type<StringType>();

        descriptor
            .Field(f => f.CheckInTime)
            .Description("Check-in time")
            .Type<StringType>();

        descriptor
            .Field(f => f.CheckOutTime)
            .Description("Check-out time")
            .Type<StringType>();
    }
}
