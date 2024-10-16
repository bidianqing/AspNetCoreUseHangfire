using AspNetCoreUseHangfire;
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
        
    }));
});
builder.Services.AddHangfireServer();

builder.Services.AddHttpClient();

builder.Services.AddScoped<IJobService, JobService>();

var app = builder.Build();

app.UseHangfireDashboard(options: new DashboardOptions
{
    Authorization = new[] { new HangfireAuthorizationFilter() }
});

app.MapControllers();

app.Run();
