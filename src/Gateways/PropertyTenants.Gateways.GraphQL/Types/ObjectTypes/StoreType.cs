using PropertyTenants.Domain.Entities.Store;

namespace PropertyTenants.Gateways.GraphQL.Types.ObjectTypes;

[ObjectType]
public class StoreType : ObjectType<Store>
{
    protected override void Configure(IObjectTypeDescriptor<Store> descriptor)
    {
        descriptor
            .Name("Store")
            .Description("A store in the system");

        descriptor
            .Field(f => f.Id)
            .Description("The unique identifier of the store")
            .Type<NonNullType<UuidType>>();

        descriptor
            .Field(f => f.Name)
            .Description("The name of the store")
            .Type<StringType>();

        descriptor
            .Field(f => f.Description)
            .Description("The description of the store")
            .Type<StringType>();

        descriptor
            .Field(f => f.CreatedAt)
            .Description("When the store was created")
            .Type<NonNullType<DateTimeType>>();

        descriptor
            .Field(f => f.LastUpdatedAt)
            .Description("When the store was last updated")
            .Type<DateTimeType>();
    }
}

[ObjectType]
public class StoreInfoType : ObjectType<StoreInfo>
{
    protected override void Configure(IObjectTypeDescriptor<StoreInfo> descriptor)
    {
        descriptor
            .Name("StoreInfo")
            .Description("Information about a store");

        descriptor
            .Field(f => f.Id)
            .Description("The unique identifier")
            .Type<NonNullType<UuidType>>();

        descriptor
            .Field(f => f.StoreId)
            .Description("The store ID")
            .Type<NonNullType<UuidType>>();

        descriptor
            .Field(f => f.Key)
            .Description("The information key")
            .Type<StringType>();

        descriptor
            .Field(f => f.Value)
            .Description("The information value")
            .Type<StringType>();

        descriptor
            .Field(f => f.CreatedAt)
            .Description("When this information was created")
            .Type<NonNullType<DateTimeType>>();
    }
}
