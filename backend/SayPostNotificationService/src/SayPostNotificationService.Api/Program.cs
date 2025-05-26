using System.Text.Json.Serialization;
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
            .AddInfrastructure(builder.Configuration)
            .ConfigureHttpJsonOptions(options => {
                options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

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

        // using (var serviceScope = app.Services.CreateScope()) {
        //     var db = serviceScope.ServiceProvider.GetRequiredService<NotificationDbContext>();
        //     db.Database.EnsureDeleted();
        //     db.Database.EnsureCreated();
        //     db.AppUsers.Add(new(new(new("01970867-a7ad-76ea-894f-2f6e6b45dc8b"))));
        //     db.AppUsers.Add(new(new(new("01970865-8808-70fc-829d-bc096bfc27f2"))));
        //     db.SaveChanges();
        // }

        app.UseCors("AllowFrontend");
        app.Run();
    }

    private static void MapHandlers(WebApplication app) {
        app.MapGroup("/notifications").MapNotificationsHandlers();
    }
}