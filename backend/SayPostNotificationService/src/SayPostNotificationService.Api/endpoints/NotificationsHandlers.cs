using ApiShared;
using ApiShared.extensions;
using MediatR;
using SayPostNotificationService.Api.contracts;
using SayPostNotificationService.Application.app_users.queries;
using SharedKernel.common.domain.ids;

namespace SayPostNotificationService.Api.endpoints;

internal static class NotificationsHandlers
{
    internal static IEndpointRouteBuilder MapNotificationsHandlers(this RouteGroupBuilder endpoints) {
        endpoints
            .WithGroupAuthenticationRequired();


        // endpoints.MapGet("/", ListNotificationsForUser);
        // endpoints.MapPost("/view", SetNotificationsAsViewed)
        //     .WithRequestValidation<ViewNotificationsRequest>();


        return endpoints;
    }
  
}