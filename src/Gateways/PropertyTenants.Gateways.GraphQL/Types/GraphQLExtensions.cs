using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;

namespace PropertyTenants.Gateways.GraphQL.Types
{
    public static class GraphQLExtensions
    {
        public static void ConfigureGraphQL(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddSubscriptionType<Subscription>()
                
                // Add data handling features
                .AddProjections()
                .AddFiltering()
                .AddSorting()
                
                // Add subscriptions
                .AddInMemorySubscriptions()
                
                // Add scalar types
                .AddType<UuidType>()
                .AddType<DateTimeType>()
                .AddType<DecimalType>();
        }
    }
}
