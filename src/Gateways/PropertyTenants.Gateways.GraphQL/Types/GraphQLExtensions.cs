using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Playground;
using HotChocolate.Data;
using HotChocolate.Types;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using PropertyTenants.Gateways.GraphQL.Types.Queries;
using PropertyTenants.Gateways.GraphQL.Types.Mutations;
using PropertyTenants.Gateways.GraphQL.Types.Subscriptions;
using PropertyTenants.Gateways.GraphQL.Types.Configuration;
using PropertyTenants.Gateways.GraphQL.DataLoaders;

namespace PropertyTenants.Gateways.GraphQL.Types
{
    public static class GraphQLExtensions
    {
        public static IServiceCollection AddAdvancedGraphQLServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add advanced GraphQL configuration
            services.Configure<AdvancedGraphQLOptions>(configuration.GetSection("GraphQL"));

            // Register all DataLoaders
            services.AddDataLoaderRegistry();
            services
                .AddScoped<PropertyDataLoader>()
                .AddScoped<UserDataLoader>()
                .AddScoped<BookingDataLoader>()
                .AddScoped<ReviewDataLoader>()
                .AddScoped<FeatureDataLoader>()
                .AddScoped<PropertyFeatureDataLoader>()
                .AddScoped<PropertiesByHostDataLoader>()
                .AddScoped<BookingsByPropertyDataLoader>()
                .AddScoped<ReviewsByPropertyDataLoader>();

            // Configure GraphQL Server with all advanced features
            var graphqlServerBuilder = services
                .AddGraphQLServer()
                
                // Core Types
                .AddQueryType<AdvancedQuery>()
                .AddMutationType<AdvancedMutation>()
                .AddSubscriptionType<AdvancedSubscription>()
                
                // Object Types
                .AddType<PropertyType>()
                .AddType<UserType>()
                .AddType<BookingType>()
                .AddType<ReviewType>()
                .AddType<FeatureType>()
                .AddType<PropertyFeatureType>()
                .AddType<AddressType>()
                .AddType<PropertyDetailType>()
                .AddType<ContactInfoType>()
                
                // Advanced Features
                .AddFiltering()
                .AddSorting()
                .AddProjections()
                .AddAuthorization()
                
                // DataLoader Support
                .AddDataLoader<PropertyDataLoader>()
                .AddDataLoader<UserDataLoader>()
                .AddDataLoader<BookingDataLoader>()
                .AddDataLoader<ReviewDataLoader>()
                .AddDataLoader<FeatureDataLoader>()
                .AddDataLoader<PropertyFeatureDataLoader>()
                .AddDataLoader<PropertiesByHostDataLoader>()
                .AddDataLoader<BookingsByPropertyDataLoader>()
                .AddDataLoader<ReviewsByPropertyDataLoader>()
                
                // Global Object Identification
                .AddGlobalObjectIdentification()
                
                // Query Complexity Analysis
                .AddQueryComplexityAnalysis(options =>
                {
                    options.MaximumAllowed = 1000;
                    options.DefaultComplexity = 1;
                    options.DefaultResolverComplexity = 5;
                    options.CreateComplexityResult = (complexity, allowedComplexity) =>
                        ComplexityResult.TooComplex(
                            $"Query complexity ({complexity}) exceeds maximum allowed complexity ({allowedComplexity})");
                })
                
                // Depth Analysis
                .AddMaxExecutionDepthRule(15)
                
                // Scalar Types
                .AddType<UuidType>()
                .AddType<DateTimeType>()
                .AddType<DecimalType>()
                
                // Introspection (disable in production)
                .ModifyRequestOptions(opt =>
                {
                    opt.IncludeExceptionDetails = !Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")?.Equals("Production", StringComparison.OrdinalIgnoreCase) ?? true;
                });

            // Redis Configuration for Subscriptions and Caching
            var redisConnectionString = configuration.GetConnectionString("Redis");
            if (!string.IsNullOrEmpty(redisConnectionString))
            {
                graphqlServerBuilder
                    .AddRedisSubscriptions(_ => StackExchange.Redis.ConnectionMultiplexer.Connect(redisConnectionString))
                    .AddQueryCaching()
                    .AddRedisQueryStorage(services => StackExchange.Redis.ConnectionMultiplexer.Connect(redisConnectionString));
            }
            else
            {
                // Fallback to in-memory for development
                graphqlServerBuilder
                    .AddInMemorySubscriptions()
                    .AddQueryCaching()
                    .AddInMemoryQueryStorage();
            }

            // Automatic Persisted Queries
            var apqEnabled = configuration.GetValue<bool>("GraphQL:AutomaticPersistedQueries:Enabled", true);
            if (apqEnabled)
            {
                graphqlServerBuilder.AddAutomaticPersistedQueryCaching();
            }

            // Add Instrumentation and Analytics
            graphqlServerBuilder
                .AddInstrumentation(opt =>
                {
                    opt.RenameRootActivity = true;
                    opt.IncludeDocument = true;
                    opt.IncludeDataLoaderKeys = true;
                    opt.RequestDetails = RequestInstrumentationScope.All;
                    opt.IncludeValidationErrors = true;
                })
                .AddApolloTracing();

            // Add JWT Authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var jwtConfig = configuration.GetSection("Authentication:AzureAdB2C");
                    options.Authority = $"https://{jwtConfig["TenantName"]}.b2clogin.com/{jwtConfig["TenantId"]}/{jwtConfig["Policy"]}/v2.0/";
                    options.Audience = jwtConfig["ClientId"];
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.FromMinutes(5)
                    };
                });

            // Add Authorization Policies
            services.AddAuthorization(options =>
            {
                options.AddPolicy("HostOrAdmin", policy =>
                    policy.RequireAssertion(context =>
                        context.User.IsInRole("Host") || 
                        context.User.IsInRole("Admin") ||
                        context.User.HasClaim("user_type", "host")));

                options.AddPolicy("AdminOnly", policy =>
                    policy.RequireRole("Admin"));

                options.AddPolicy("VerifiedUser", policy =>
                    policy.RequireAuthenticatedUser()
                          .RequireClaim("email_verified", "true"));
            });

            return services;
        }

        public static void ConfigureGraphQL(this IServiceCollection services, IConfiguration configuration)
        {
            // Backward compatibility - calls new advanced method
            services.AddAdvancedGraphQLServices(configuration);
        }

        public static IApplicationBuilder UseAdvancedGraphQL(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Enable GraphQL Playground in development
            if (env.IsDevelopment())
            {
                app.UseGraphQLPlayground(new PlaygroundOptions
                {
                    QueryPath = "/graphql",
                    Path = "/playground",
                    BetaUpdates = true,
                    EnableSubscription = true,
                    EnableIntrospection = true
                });
            }

            // Map GraphQL endpoint with WebSocket support for subscriptions
            app.UseWebSockets();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL("/graphql")
                    .WithOptions(new GraphQLServerOptions
                    {
                        EnableSchemaRequests = env.IsDevelopment(),
                        EnableGetRequests = env.IsDevelopment(),
                        EnableMultipartRequests = true
                    });
            });

            return app;
        }
    }
}
