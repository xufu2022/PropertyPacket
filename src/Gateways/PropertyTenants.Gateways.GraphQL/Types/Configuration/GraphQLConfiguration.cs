namespace PropertyTenants.Gateways.GraphQL.Types.Configuration
{
    public static class GraphQLConfiguration
    {
        public static void ConfigureGraphQL(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddGraphQLServer()
                .AddQueryType<PropertyTenants.Gateways.GraphQL.Types.Queries.Query>()
                .AddMutationType<PropertyTenants.Gateways.GraphQL.Types.Mutations.Mutation>()
                .AddType<PropertyTenants.Gateways.GraphQL.Types.ObjectTypes.FeatureType>()
                .AddType<PropertyTenants.Gateways.GraphQL.Types.ObjectTypes.FeatureGroupType>()
                .AddFiltering()
                .AddSorting()
                .AddProjections()
                .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true)
                .InitializeOnStartup();
        }
    }
}
