using ApiShared;
using ApiShared.extensions;
using MediatR;
using SayPostNotificationService.Api.contracts;
using SayPostNotificationService.Application.app_users.commands;
using SayPostNotificationService.Application.app_users.queries;
using SayPostNotificationService.Domain.app_user_aggregate;
using SharedKernel.common.domain.ids;

namespace SayPostNotificationService.Api.endpoints;

internal static class NotificationsHandlers
{
    internal static IEndpointRouteBuilder MapNotificationsHandlers(this RouteGroupBuilder endpoints) {
        endpoints
            .WithGroupAuthenticationRequired();


        endpoints.MapGet("/", ListNotificationsForUser);
        endpoints.MapPatch("/view", SetNotificationsAsViewed)
            .WithRequestValidation<ViewNotificationsRequest>();


        return endpoints;
    }

    private static async Task<IResult> ListNotificationsForUser(
        HttpContext httpContext, ISender mediator, bool showOnlyNotViewed = false
    ) {
        AppUserId userId = httpContext.GetAuthenticatedUserId();

        ListNotificationsForUserQuery query = new(userId, showOnlyNotViewed);
        var result = await mediator.Send(query);

        return CustomResults.FromErrOr(result,
            (notifications) => Results.Json(new {
                Notifications = notifications.Select(NotificationsDataResponse.FromNotification).ToArray()
            })
        );
    }

    private static async Task<IResult> SetNotificationsAsViewed(
        HttpContext httpContext, ISender mediator
    ) {
        AppUserId userId = httpContext.GetAuthenticatedUserId();
        var request = httpContext.GetValidatedRequest<ViewNotificationsRequest>();

        SetNotificationsAsViewedCommand command = new(userId, request.GetParsedNotificationId());
        var result = await mediator.Send(command);

        return CustomResults.FromErrOrNothing(result, () => Results.Ok());
    }
}