using Microsoft.AspNetCore.Builder;
using SayPostAuthService.Infrastructure.middleware.eventual_consistency_middleware;

namespace SayPostAuthService.Infrastructure;

public static class RequestPipeline
{
    public static IApplicationBuilder AddInfrastructureMiddleware(this IApplicationBuilder app) {
        app.UseMiddleware<EventualConsistencyMiddleware>();
        return app;
    }
}
