using ApiShared;
using ApiShared.extensions;
using MediatR;
using SharedKernel.common.domain.ids;

namespace SayPostAuthService.Api.endpoints;

internal static class AppUsersHandlers
{
    public static IEndpointRouteBuilder MapAppUsersHandlers(this RouteGroupBuilder endpoints) {
        endpoints.MapGet("/usernames", GetUsersUsernames);

        return endpoints;
    }
    private static async Task<IResult> GetUsersUsernames(
        HttpContext httpContext, ISender mediator, string[] userIds
    ) {
        GetUsernamesQuery query = new(userId);
        var result = await mediator.Send(query);

        return CustomResults.FromErrOr(result, (vm) => Results.Json());
    }
}