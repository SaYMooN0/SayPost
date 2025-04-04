using ApiShared;
using SayPostAuthService.Api.endpoints;
using SayPostAuthService.Application;
using SayPostAuthService.Infrastructure;

namespace SayPostAuthService.Api;

public class Program
{
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);
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
        app.UseHttpsRedirection();


        MapHandlers(app);

        app.Run();
    }
    
    private static void MapHandlers(WebApplication app) {
        app.MapRootHandlers();
    }
    
}