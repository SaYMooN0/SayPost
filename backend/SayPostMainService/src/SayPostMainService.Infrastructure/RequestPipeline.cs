using InfrastructureShared;
using Microsoft.AspNetCore.Builder;
using SayPostMainService.Infrastructure.persistence;

namespace SayPostMainService.Infrastructure;

public static class RequestPipeline
{
    public static IApplicationBuilder AddInfrastructureMiddleware(this IApplicationBuilder app) {
        app.UseMiddleware<EventualConsistencyMiddleware<MainDbContext>>();
        return app;
    }
}