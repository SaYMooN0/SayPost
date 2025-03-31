using SharedKernel.common.errs;

namespace SayPostMainService.Api;

public class Program
{
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment()) {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app
            .MapGet("/greet", (HttpContext _) => new { Msg = "main service" })
            .WithName("greet");
        app
            .MapGet("/test-err-with-extra", (HttpContext _) => Results.BadRequest(new[] {
                    new ErrWithExtraData("Validation failed", new Dictionary<string, object> {
                            ["just some field"] = "error",
                            ["int field"] = 123
                        }, details: "Smth is invalid"
                    )
                }
            ));
        app.Run();
    }
}