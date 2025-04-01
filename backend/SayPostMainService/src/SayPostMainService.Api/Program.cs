using ApiShared;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

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
            .MapGet("/test", (HttpContext _) => Test());
        app.Run();
    }

    public static IResult Test() {
        ErrList res = new();
        res.Add(new Err("Basic err"));
        res.AddPossibleErr(ErrOr<string>.Error(new Err("possible basic err")));
        res.AddPossibleErr(ErrOr<string>.Success("success string value"));

        ErrWithExtraData errWithExtraData1 = new ErrWithExtraData(
            "basic err with extra data", new() {
                { "key1", "data" },
                { "key2", new { Field1 = "value1" } }
            }
        );
        ErrWithExtraData errWithExtraData2 = new ErrWithExtraData(
            "possible err with extra data", new() {
                { "key1", 123 },
                { "key2", new List<string> { "value1", "value2" } }
            }
        );

        res.AddPossibleErr(ErrOr<string>.Error(errWithExtraData1));
        res.Add(errWithExtraData2);
        return CustomResults.ErrorResponse(res);
    }
}