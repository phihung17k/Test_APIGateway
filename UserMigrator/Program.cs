using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UserService.Data;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        string connectionString = Environment.GetEnvironmentVariable("ConnectionString_UserDB") ?? context.Configuration.GetConnectionString("ConnectionString_UserDB") ?? string.Empty;
        Console.WriteLine("-------- UserService.Migration connectionString: {0}", connectionString);
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(connectionString, new MySqlServerVersion(new Version(9, 2, 0))));
    })
    .Build();

using (var scope = host.Services.CreateScope())
{
    ApplicationDbContext? dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
    Console.WriteLine("-------- UserService.Migration ApplicationDbContext: {0}", dbContext);
    dbContext?.Database.Migrate();
}


Task task = host.RunAsync();
if (await Task.WhenAny(task, Task.Delay(30000)) != task)
{
    Console.WriteLine("Done");
    Environment.Exit(0);
}