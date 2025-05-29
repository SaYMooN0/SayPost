using InfrastructureShared;
using Microsoft.AspNetCore.Builder;
using SayPostFollowingsService.Infrastructure.persistence;

namespace SayPostFollowingsService.Infrastructure;

public static class RequestPipeline
{
    public static IApplicationBuilder AddInfrastructureMiddleware(this IApplicationBuilder app) {
        app.UseMiddleware<EventualConsistencyMiddleware<FollowingDbContext>>();
        return app;
    }
}