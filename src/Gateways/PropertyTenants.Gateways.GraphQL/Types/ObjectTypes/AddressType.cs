using PropertyTenants.Domain.Entities.Common;

namespace PropertyTenants.Gateways.GraphQL.Types.ObjectTypes;

[ObjectType]
public class AddressType : ObjectType<Address>
{
    protected override void Configure(IObjectTypeDescriptor<Address> descriptor)
    {
        descriptor
            .Name("Address")
            .Description("An address location");

        descriptor
            .Field(f => f.Id)
            .Description("The unique identifier of the address")
            .Type<NonNullType<IntType>>();

        descriptor
            .Field(f => f.Street)
            .Description("Street address")
            .Type<StringType>();

        descriptor
            .Field(f => f.City)
            .Description("City name")
            .Type<StringType>();

        descriptor
            .Field(f => f.State)
            .Description("State or province")
            .Type<StringType>();

        descriptor
            .Field(f => f.ZipCode)
            .Description("Postal/ZIP code")
            .Type<StringType>();

        descriptor
            .Field(f => f.Country)
            .Description("Country name")
            .Type<StringType>();

        descriptor
            .Field(f => f.Email)
            .Description("Contact email")
            .Type<StringType>();

        descriptor
            .Field(f => f.PhoneNumber)
            .Description("Phone number")
            .Type<StringType>();

        descriptor
            .Field(f => f.Mobile)
            .Description("Mobile number")
            .Type<StringType>();

        // Computed field
        descriptor
            .Field("fullAddress")
            .Description("Complete formatted address")
            .Type<StringType>()
            .Resolve(context =>
            {
                var address = context.Parent<Address>();
                var parts = new[] { address.Street, address.City, address.State, address.ZipCode, address.Country }
                    .Where(s => !string.IsNullOrWhiteSpace(s));
                return string.Join(", ", parts);
            });
    }
}
