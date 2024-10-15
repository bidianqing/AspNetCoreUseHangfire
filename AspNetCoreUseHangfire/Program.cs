using Domain.Services;
using Hangfire;
using Hangfire.MySql;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMediatR(xfg =>
{
    xfg.RegisterServicesFromAssemblies(Assembly.Load("Domain"), Assembly.Load("AspNetCoreUseHangfire"));
});
builder.Services.AddHangfire(options =>
{
    options.UseStorage(new MySqlStorage(builder.Configuration.GetConnectionString("MySqlConnectionString"), new MySqlStorageOptions
    {
        TablesPrefix = "hangfire"
    }));
});
builder.Services.AddHangfireServer();

builder.Services.AddHttpClient();

builder.Services.AddScoped<IJobService, JobService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHangfireDashboard();

app.MapControllers();

app.Run();
