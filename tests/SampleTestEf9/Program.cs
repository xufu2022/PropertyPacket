using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PropertyPacket.Infrastructure;
using SampleTestEf9;

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
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), x=>x.UseHierarchyId())
                    .EnableSensitiveDataLogging()
            .LogTo(
                s =>
                {
                    //if (LoggingEnabled)
                    //{
                        Console.WriteLine(s);
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
#region "EnsureDatabaseCreated"
//using (var scope = host.Services.CreateScope())
//{
//    var dbContext = scope.ServiceProvider.GetRequiredService<PropertyPacketContext>();
//    //await HierarchyIdSample.Seed(dbContext);
//    //await dbContext.SaveChangesAsync();

//    var level = 0;
//    while (true)
//    {
//        #region GetLevel
//        var generation = await dbContext.CategoryHierarchies.Where(halfling => halfling.Path.GetLevel() == level).ToListAsync();
//        #endregion

//        if (!generation.Any())
//        {
//            break;
//        }

//        Console.Write($"Generation {level}: ");

//        for (var i = 0; i < generation.Count; i++)
//        {
//            var halfling = generation[i];
//            Console.Write($"{halfling.Name}");
//            if (i < generation.Count - 1)
//            {
//                Console.Write(", ");
//            }
//        }

//        Console.WriteLine();

//        level++;
//    }

//    Console.WriteLine();

//}
#endregion

#region "FindDirectAncestor"
using (var scope = host.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<PropertyPacketContext>();
    var directAncestor = (await HierarchyIdSample.FindDirectAncestor(dbContext,"Games"))!;
    Console.WriteLine();
    Console.WriteLine($"The direct ancestor of Games is {directAncestor.Name}");
}
#endregion

#region "FindAllAncestors"
using (var scope = host.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<PropertyPacketContext>();
    var ancestors = HierarchyIdSample.FindAllAncestors(dbContext, "Outdoors").ToList();
    Console.WriteLine();
    Console.WriteLine("All ancestors of Games:");
    foreach (var ancestor in ancestors)
    {
        Console.WriteLine(ancestor.Name);
    }
}
#endregion

#region "FindDirectDescendents"
using (var scope = host.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<PropertyPacketContext>();
    var descendants = HierarchyIdSample.FindDirectDescendents(dbContext, "Games").ToList();
    Console.WriteLine();
    Console.WriteLine("Direct descendants of Games:");
    foreach (var descendant in descendants)
    {
        Console.WriteLine(descendant.Name);
    }
}
#endregion

