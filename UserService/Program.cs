using Microsoft.EntityFrameworkCore;
using UserService.Data;
using UserService.Services;
using UserService.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IUserServices, UserServices>();
string connectionString = Environment.GetEnvironmentVariable("ConnectionString_UserDB") ?? builder.Configuration.GetConnectionString("ConnectionString_UserDB") ?? string.Empty;
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString: connectionString, serverVersion: new MySqlServerVersion(new Version(9, 2, 0)))
           .LogTo(Console.WriteLine, LogLevel.Debug)
           .EnableSensitiveDataLogging()
           .EnableDetailedErrors()
);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
