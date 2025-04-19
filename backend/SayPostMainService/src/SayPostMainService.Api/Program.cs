using ApiShared;
using ApiShared.extensions;
using SayPostMainService.Api.endpoints;
using SayPostMainService.Application;
using SayPostMainService.Domain.app_user_aggregate;
using SayPostMainService.Infrastructure;
using SayPostMainService.Infrastructure.persistence;
using Serilog;

namespace SayPostMainService.Api;

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

        app.UseCors("AllowFrontend");
        app.Run();
    }

    private static void MapHandlers(WebApplication app) {
        app.MapGroup("/draft-posts").MapDraftPostHandlers();
    }
}