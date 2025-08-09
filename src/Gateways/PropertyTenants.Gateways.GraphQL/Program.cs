using PropertyTenants.Gateways.GraphQL.Types;
using PropertyTenants.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add Entity Framework
builder.Services.AddDbContext<PropertyTenantsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure GraphQL
builder.Services.ConfigureGraphQL(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseWebSockets();
app.UseRouting();

app.MapGraphQL();

app.Run();
