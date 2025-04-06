using InfrastructureShared;
using Microsoft.AspNetCore.Builder;
using SayPostAuthService.Infrastructure.persistence;

namespace SayPostAuthService.Infrastructure;

public static class RequestPipeline
{
    public static IApplicationBuilder AddInfrastructureMiddleware(this IApplicationBuilder app) {
        app.UseMiddleware<EventualConsistencyMiddleware<AuthDbContext>>();
        return app;
    }
}