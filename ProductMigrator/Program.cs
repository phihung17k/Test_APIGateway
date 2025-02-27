using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductService.Data;

//var webHost = new WebHostBuilder().UseContentRoot(Directory.GetCurrentDirectory())
//                                  .UseStartup<ConsoleStartup>()
//                                  .Build();
//using (ApplicationDbContext? context = (ApplicationDbContext?) webHost.Services.GetService(typeof(ApplicationDbContext)))
//{
//    Console.WriteLine("ProductService.Migration: ApplicationDbContext = {0}", context);
//    context?.Database.Migrate();
//}
//Console.WriteLine("Done");

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        Console.WriteLine("ProductService.Migration ConnectionString_ProductDB: {0}", context.Configuration.GetConnectionString("ConnectionString_ProductDB"));
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(context.Configuration.GetConnectionString("ConnectionString_ProductDB"),
                new MySqlServerVersion(new Version(9, 2, 0))));
    })
    .Build();

using (var scope = host.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    //dbContext.Database.EnsureCreated();
    dbContext.Database.Migrate();
}

host.Run();
Console.WriteLine("Done");

//class ConsoleStartup
//{
//    public IConfiguration Configuration { get; }

//    public ConsoleStartup()
//    {
//        var builder = new ConfigurationBuilder()
//            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
//            .AddEnvironmentVariables();
//        Configuration = builder.Build();
//        Console.WriteLine("ConnectionString_ProductDB: {0}", Configuration.GetConnectionString("ConnectionString_ProductDB"));
//    }

//    public void ConfigureServices(IServiceCollection services)
//    {
//        services.AddDbContext<ApplicationDbContext>(options =>
//        {
//            options.UseMySql(connectionString: Configuration.GetConnectionString("ConnectionString_ProductDB"), serverVersion: new MySqlServerVersion(new Version(9, 2, 0)));

//        });
//    }

//    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//    {

//    }
//}