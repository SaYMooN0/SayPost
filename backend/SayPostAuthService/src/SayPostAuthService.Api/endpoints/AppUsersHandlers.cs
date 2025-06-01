using ApiShared.extensions;
using MediatR;
using SayPostAuthService.Api.contracts.app_users;
using SayPostAuthService.Application.app_users.queries;

namespace SayPostAuthService.Api.endpoints;

internal static class AppUsersHandlers
{
    public static IEndpointRouteBuilder MapAppUsersHandlers(this RouteGroupBuilder endpoints) {
        endpoints.MapPost("/usernames-by-ids", GetUsersUsernames)
            .WithRequestValidation<GetUsernamesByIdsRequest>();

        return endpoints;
    }

    private static async Task<IResult> GetUsersUsernames(
        HttpContext httpContext, ISender mediator
    ) {
        var request = httpContext.GetValidatedRequest<GetUsernamesByIdsRequest>();

        GetUsernamesQuery query = new(request.ParsedIds);
        var result = await mediator.Send(query);

        return Results.Json(result);
    }
}