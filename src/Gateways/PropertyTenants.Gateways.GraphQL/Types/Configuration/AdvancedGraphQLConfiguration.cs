using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using HotChocolate.Subscriptions;
using HotChocolate.Authorization;
using PropertyTenants.Gateways.GraphQL.Types.ObjectTypes;
using PropertyTenants.Gateways.GraphQL.Types.InputTypes;
using PropertyTenants.Gateways.GraphQL.Types.EnumTypes;
using PropertyTenants.Gateways.GraphQL.Types.InterfaceTypes;
using PropertyTenants.Gateways.GraphQL.Types.UnionTypes;
using PropertyTenants.Gateways.GraphQL.Types.Errors;

namespace PropertyTenants.Gateways.GraphQL.Types.Configuration
{
    public static class AdvancedGraphQLConfiguration
    {
        public static IServiceCollection AddAdvancedGraphQLServer(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddGraphQLServer()
                
                // Core GraphQL types
                .AddQueryType<Queries.Query>()
                .AddMutationType<Mutations.Mutation>()
                .AddSubscriptionType<Subscriptions.Subscription>()
                
                // Object Types - Advanced entity representations
                .AddType<PropertyType>()
                .AddType<UserType>()
                .AddType<BookingType>()
                .AddType<ReviewType>()
                .AddType<AddressType>()
                .AddType<PropertyDetailType>()
                .AddType<PropertyFeatureType>()
                .AddType<FeatureType>()
                .AddType<FileEntryType>()
                .AddType<RoleType>()
                .AddType<StoreType>()
                
                // Input Types - Advanced input handling
                .AddType<CoreInputTypes.CreatePropertyInput>()
                .AddType<CoreInputTypes.UpdatePropertyInput>()
                .AddType<CoreInputTypes.CreateUserInput>()
                .AddType<CoreInputTypes.UpdateUserInput>()
                .AddType<CoreInputTypes.CreateBookingInput>()
                .AddType<CoreInputTypes.PropertyFilterInput>()
                .AddType<CoreInputTypes.UserFilterInput>()
                .AddType<CoreInputTypes.BookingFilterInput>()
                
                // Enum Types - Advanced type safety
                .AddType<PropertyTypeEnum>()
                .AddType<PropertyStatusType>()
                .AddType<BookingStatusType>()
                
                // Interface Types - Advanced polymorphism
                .AddType<IAuditableInterface>()
                .AddType<IPropertyInterface>()
                
                // Union Types - Advanced type unions
                .AddType<PropertyUnion>()
                
                // Data Layer Features - Advanced querying capabilities
                .AddProjections()
                .AddFiltering()
                .AddSorting()
                .AddPagination()
                
                // Advanced Subscriptions
                .AddInMemorySubscriptions()
                .AddSubscriptionType<Subscriptions.Subscription>()
                
                // Advanced Authorization
                .AddAuthorization()
                
                // Advanced Scalar Types
                .AddType<UuidType>()
                .AddType<DateTimeType>()
                .AddType<DecimalType>()
                .AddType<ByteType>()
                .AddType<LongType>()
                .AddType<UrlType>()
                .AddType<EmailAddressType>()
                .AddType<PhoneNumberType>()
                
                // Advanced Error Handling
                .AddErrorFilter<GlobalErrorFilter>()
                .AddDiagnosticEventListener<DiagnosticEventListener>()
                
                // Advanced Performance Features
                .AddDataLoader<PropertyDataLoader>()
                .AddDataLoader<UserDataLoader>()
                .AddDataLoader<BookingDataLoader>()
                .AddDataLoader<ReviewDataLoader>()
                
                // Advanced Caching
                .AddRedisQueryStorage() // Redis for distributed caching
                .AddQueryCachingPolicy()
                
                // Advanced Request Execution
                .ModifyRequestOptions(opt =>
                {
                    opt.IncludeExceptionDetails = configuration.GetValue<bool>("GraphQL:IncludeExceptionDetails", true);
                    opt.ExecutionTimeout = TimeSpan.FromMinutes(configuration.GetValue<int>("GraphQL:ExecutionTimeoutMinutes", 5));
                    opt.MaxExecutionDepth = configuration.GetValue<int>("GraphQL:MaxExecutionDepth", 15);
                    opt.MaxExecutionComplexity = configuration.GetValue<int>("GraphQL:MaxExecutionComplexity", 1000);
                })
                
                // Advanced Schema Options
                .ModifyOptions(opt =>
                {
                    opt.SortCompatibilityMode = SortCompatibilityMode.Sequence;
                    opt.DefaultQueryDependencyInjectionScope = DependencyInjectionScope.Request;
                    opt.EnableDefer = true;
                    opt.EnableStream = true;
                    opt.UseXmlDocumentation = true;
                    opt.RemoveUnreachableTypes = true;
                    opt.StrictValidation = true;
                })
                
                // Advanced Instrumentation
                .AddInstrumentation(opt =>
                {
                    opt.IncludeDocument = true;
                    opt.IncludeVariables = true;
                    opt.RequestDetails = RequestDetails.All;
                })
                
                // Advanced Type Discovery
                .AddTypeExtension<PropertyTypeExtension>()
                .AddTypeExtension<UserTypeExtension>()
                .AddTypeExtension<BookingTypeExtension>()
                
                // Advanced Middleware
                .UsePersistedQueryPipeline()
                .UseAutomaticPersistedQueryPipeline()
                .UseReadOnlyFileSystem()
                .UseDefaultPipeline();

            return services;
        }
        
        public static IServiceCollection AddAdvancedGraphQLDataLoaders(this IServiceCollection services)
        {
            // Advanced DataLoader registrations for optimal N+1 query prevention
            services.AddScoped<PropertyDataLoader>();
            services.AddScoped<UserDataLoader>();
            services.AddScoped<BookingDataLoader>();
            services.AddScoped<ReviewDataLoader>();
            services.AddScoped<FeatureDataLoader>();
            services.AddScoped<PropertyFeatureDataLoader>();
            
            return services;
        }
        
        public static IServiceCollection AddAdvancedGraphQLSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            // Advanced security configurations
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
                options.AddPolicy("HostOnly", policy => policy.RequireRole("Host"));
                options.AddPolicy("GuestOnly", policy => policy.RequireRole("Guest"));
                options.AddPolicy("HostOrAdmin", policy => policy.RequireRole("Host", "Admin"));
            });
            
            return services;
        }
    }
}
