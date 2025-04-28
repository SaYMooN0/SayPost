using InfrastructureShared;
using Microsoft.AspNetCore.Builder;
using SayPostNotificationService.Infrastructure.persistence;

namespace SayPostNotificationService.Infrastructure;

public static class RequestPipeline
{
    public static IApplicationBuilder AddInfrastructureMiddleware(this IApplicationBuilder app) {
        app.UseMiddleware<EventualConsistencyMiddleware<NotificationDbContext>>();
        return app;
    }
}