using HotChocolate.Data;

namespace PropertyTenants.Gateways.GraphQL.Types.Configuration
{
    public static class GraphQLConfiguration
    {
        public static void ConfigureGraphQL(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddGraphQLServer()
                .AddQueryType<PropertyTenants.Gateways.GraphQL.Types.Queries.Query>()
                .AddMutationType<PropertyTenants.Gateways.GraphQL.Types.Mutations.Mutation>()
                .AddSubscriptionType<PropertyTenants.Gateways.GraphQL.Types.Subscriptions.Subscription>()
                
                // Add object types
                .AddType<PropertyTenants.Gateways.GraphQL.Types.ObjectTypes.PropertyType>()
                .AddType<PropertyTenants.Gateways.GraphQL.Types.ObjectTypes.BookingType>()
                .AddType<PropertyTenants.Gateways.GraphQL.Types.ObjectTypes.UserType>()
                .AddType<PropertyTenants.Gateways.GraphQL.Types.ObjectTypes.ReviewType>()
                
                // Add enum types
                .AddType<PropertyTenants.Gateways.GraphQL.Types.EnumTypes.PropertyStatusEnum>()
                .AddType<PropertyTenants.Gateways.GraphQL.Types.EnumTypes.PropertyTypeEnum>()
                
                // Add input types
                .AddType<PropertyTenants.Gateways.GraphQL.Types.InputTypes.CreatePropertyInput>()
                .AddType<PropertyTenants.Gateways.GraphQL.Types.InputTypes.CreateBookingInput>()
                .AddType<PropertyTenants.Gateways.GraphQL.Types.InputTypes.CreateUserInput>()
                
                // Add scalar types
                .AddType<DateTimeType>()
                .AddType<DecimalType>()
                .AddType<UuidType>()
                
                // Add advanced features
                .AddFiltering()
                .AddSorting()
                .AddProjections()
                .AddInMemorySubscriptions()
                .ModifyRequestOptions(opt =>
                {
                    opt.IncludeExceptionDetails = configuration.GetValue<bool>("GraphQL:IncludeExceptionDetails", true);
                    opt.ExecutionTimeout = TimeSpan.FromMinutes(5);
                });
        }
    }
}
