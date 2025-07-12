
// set up database connection

using PropertyPacket.Infrastructure;

IHostBuilder builder = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        config.SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true);
    })
    .ConfigureServices((hostContext, services) =>
    {
        // Access the configuration
        var configuration = hostContext.Configuration;
        services.AddDbContext<PropertyPacketContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), x => x.UseHierarchyId())
                    .EnableSensitiveDataLogging()
            .LogTo(
                s =>
                {
                    //if (LoggingEnabled)
                    //{
                    //    Console.WriteLine(s);
                    //}
                }, LogLevel.Information));

        // Register the custom service
        try
        {
            services.BuildServiceProvider(new ServiceProviderOptions
            {
                ValidateScopes = true,
                ValidateOnBuild = true
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Dependency Injection validation failed: {ex.Message}");
            throw;
        }

    });

IHost host = builder.Build();

// ensure the database is created
using (var scope = host.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<PropertyPacketContext>();
    dbContext.Database.EnsureCreated();
}

await host.StopAsync();

Console.WriteLine("Database connection established successfully.");
