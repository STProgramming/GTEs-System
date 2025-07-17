using GTEs_BE.Datas;
using GTEs_BE.Hubs;
using GTEs_BE.Interfaces.IService;
using GTEs_BE.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddHostedService<StartUpService>();

builder.Services.AddSignalR();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContextFactory<ApplicationContext>(options =>
{
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(10, 11, 11)));
});

builder.Services.AddTransient<IHabitsService, HabitsService>();

builder.Services.AddTransient<INotificationsService, NotificationsService>();

builder.Services.AddTransient<ISystemSettingsService, SystemSettingsService>();

builder.Services.AddTransient<ITripsService, TripsService>();

builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<NotificationsHub>("/notificationshub");

app.Run();
