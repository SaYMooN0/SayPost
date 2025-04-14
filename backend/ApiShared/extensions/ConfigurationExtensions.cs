using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.OpenTelemetry;

namespace ApiShared.extensions;

public static class ConfigurationExtensions
{
    public static void ConfigureSerilog(IConfiguration configuration) {
        var serviceName = configuration["ServiceName"]
                          ?? throw new ArgumentNullException("ServiceName is not provided");

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.OpenTelemetry(options => {
                options.Endpoint = "http://localhost:4317";
                options.Protocol = OtlpProtocol.Grpc;
                options.RestrictedToMinimumLevel = LogEventLevel.Verbose;
                options.ResourceAttributes = new Dictionary<string, object> {
                    ["service.name"] = serviceName
                };
            })
            .CreateLogger();
    }
}