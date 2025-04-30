using ApiShared;
using ApiShared.extensions;
using SayPostNotificationService.Api.endpoints;
using SayPostNotificationService.Application;
using SayPostNotificationService.Infrastructure;
using SayPostNotificationService.Infrastructure.persistence;

namespace SayPostNotificationService.Api;

public class Program
{
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        builder.ConfigureLogging();

        builder.Services.AddCors(options => {
            options.AddPolicy("AllowFrontend", policy => {
                policy
                    .WithOrigins("http://localhost:5173")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        builder.Services.AddOpenApi();
        builder.Services
            .AddApplication(builder.Configuration)
            .AddInfrastructure(builder.Configuration);

        var app = builder.Build();
        app.AddInfrastructureMiddleware();

        if (app.Environment.IsDevelopment()) {
            app.MapOpenApi();
        }
        else {
            app.UseHttpsRedirection();
        }

        app.AddExceptionHandlingMiddleware();

        MapHandlers(app);

        using (var serviceScope = app.Services.CreateScope()) {
            var db = serviceScope.ServiceProvider.GetRequiredService<NotificationDbContext>();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            // db.AppUsers.Add(new(new(new("01964f73-b7e9-71c7-8b45-9f63b58df9e6"))));
            db.AppUsers.Add(new(new(new("0196405c-0c03-7520-8da6-d17cdc334ba7"))));
            db.SaveChanges();
        }
        
        app.UseCors("AllowFrontend");
        app.Run();
    }

    private static void MapHandlers(WebApplication app) {
        app.MapGroup("/notifications").MapNotificationsHandlers();
    }
}