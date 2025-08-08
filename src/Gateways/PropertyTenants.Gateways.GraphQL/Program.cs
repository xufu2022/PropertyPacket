using PropertyTenants.Gateways.GraphQL.Types.Mutations;
using PropertyTenants.Gateways.GraphQL.Types.Queries;
using PropertyTenants.Gateways.GraphQL.Types.ObjectTypes;
using PropertyTenants.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationInsightsTelemetry();

// Add Entity Framework
builder.Services.AddDbContext<PropertyTenantsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories - using direct EF context instead
// builder.Services.AddScoped<IFeatureRepository, FeatureRepository>();
// builder.Services.AddScoped<IFeatureGroupRepository, FeatureGroupRepository>();

builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddType<FeatureType>()
    .AddType<FeatureGroupType>()
    .AddFiltering()
    .AddSorting()
    .AddProjections()
    .InitializeOnStartup();


builder.Services.AddCors();

var app = builder.Build();
app.UseCors(c => c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGraphQL();
app.Run();
