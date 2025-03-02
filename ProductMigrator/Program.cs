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
        string connectionString = Environment.GetEnvironmentVariable("ConnectionString_ProductDB") ?? context.Configuration.GetConnectionString("ConnectionString_ProductDB") ?? string.Empty;
        Console.WriteLine("-------- ProductService.Migration connectionString: {0}", connectionString);
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(connectionString, new MySqlServerVersion(new Version(9, 2, 0))));
    })
    .Build();

using (var scope = host.Services.CreateScope())
{
    ApplicationDbContext? dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
    Console.WriteLine("-------- ProductService.Migration ApplicationDbContext: {0}", dbContext);
    //dbContext.Database.EnsureCreated();
    dbContext?.Database.Migrate();
}


Task task = host.RunAsync();
if (await Task.WhenAny(task, Task.Delay(30000)) != task)
{
    Console.WriteLine("Done");
    Environment.Exit(0);
    //return;
}

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