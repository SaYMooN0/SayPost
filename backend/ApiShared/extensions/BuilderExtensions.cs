using Microsoft.AspNetCore.Builder;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Grafana.Loki;

namespace ApiShared.extensions;

public static class BuilderExtensions
{
    public static void ConfigureLogging(this WebApplicationBuilder builder) {
        var serviceName = builder.Configuration["ServiceName"]
                          ?? throw new ArgumentNullException("ServiceName is not provided");

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .Enrich.WithProperty("Application", "VokimiBackend")
            .Enrich.WithProperty("Environment", builder.Environment.EnvironmentName)
            .WriteTo.Console()
            .WriteTo.GrafanaLoki(
                "http://localhost:3100",
                labels: [new LokiLabel() { Key = "app", Value = serviceName }])
            .WriteTo.Console()
            .CreateLogger();
        
        builder.Host.UseSerilog();
    }
}