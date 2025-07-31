using PropertyTenants.Gateways.GraphQL.Types.Mutations;
using PropertyTenants.Gateways.GraphQL.Types.Queries;
using PropertyTenants.Persistence;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationInsightsTelemetry();

builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    //.AddMutationType<Mutation>()
    .AddFiltering()
    .AddSorting()
    .AddProjections()
    .InitializeOnStartup();


//
//.AddMutationType<Mutation>()
//.AddSubscriptionType<Subscription>()
//.AddType<FileEntryType>()
//.AddType<FileEntryInputType>()
//.AddType<AuditLogEntryType>()
//.AddType<OutboxEventType>()
//.AddFiltering()
//.AddSorting()
//.AddProjections();
//.AddGraphQLConventions();

builder.Services.AddCors();

var app = builder.Build();
app.UseCors(c => c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();
app.MapGraphQL();

//app.RunWithGraphQLCommands(args);
app.Run();
