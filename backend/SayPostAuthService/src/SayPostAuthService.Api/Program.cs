using ApiShared;
using SayPostAuthService.Api.endpoints;
using SayPostAuthService.Application;
using SayPostAuthService.Infrastructure;

namespace SayPostAuthService.Api;

public class Program
{
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend", policy =>
            {
                policy.WithOrigins("http://localhost:5173")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });
        
        builder.Services.AddOpenApi();
        builder.Services
            .AddAuthTokenConfig(builder.Configuration)
            .AddApplication(builder.Configuration)
            .AddInfrastructure(builder.Configuration);
        var app = builder.Build();
        app.AddInfrastructureMiddleware();

        if (app.Environment.IsDevelopment()) {
            app.MapOpenApi();
        }

        
        app.AddExceptionHandlingMiddleware();
        if (!app.Environment.IsDevelopment()) {
            app.UseHttpsRedirection();
        }
        MapHandlers(app);
        
        app.UseCors("AllowFrontend");
        app.Run();
    }

    private static void MapHandlers(WebApplication app) {
        app.MapRootHandlers();
    }
}