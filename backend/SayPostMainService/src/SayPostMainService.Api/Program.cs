using System.Text.Json.Serialization;
using ApiShared;
using ApiShared.extensions;
using SayPostMainService.Api.endpoints;
using SayPostMainService.Application;
using SayPostMainService.Application.interfaces;
using SayPostMainService.Domain.app_user_aggregate;
using SayPostMainService.Infrastructure;
using SayPostMainService.Infrastructure.persistence;
using SharedKernel.common.domain.ids;
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

        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddScoped<ICurrentActorProvider, HttpContextCurrentUserProvider>();
        builder.Services
            .AddApplication()
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
        //     var db = serviceScope.ServiceProvider.GetRequiredService<MainDbContext>();
        //     db.Database.EnsureDeleted();
        //     db.Database.EnsureCreated();
        //     db.AppUsers.Add(new AppUser(new AppUserId(new Guid("01970865-8808-70fc-829d-bc096bfc27f2"))));
        //     db.AppUsers.Add(new AppUser(new AppUserId(new Guid("01970867-a7ad-76ea-894f-2f6e6b45dc8b"))));
        //     db.SaveChanges();
        // }

        app.UseCors("AllowFrontend");
        app.Run();
    }

    private static void MapHandlers(WebApplication app) {   
        app.MapGroup("/posts").MapPostsHandlers();
        app.MapGroup("/draft-posts").MapDraftPostsHandlers();
        app.MapGroup("/draft-posts/{draftPostId}").MapSpecificDraftPostHandlers();
        app.MapGroup("/post-tags").MapPostTagsHandlers();
        app.MapGroup("/posts/{postId}").MapSpecificPostHandlers();
        app.MapGroup("/users/{userId}").MapSpecificAppUserHandlers();
        app.MapGroup("/users/{userId}/statistics").MapAppUserStatisticsHandlers();
        app.MapGroup("/profile").MapUserProfileEndpoints();
    }
}