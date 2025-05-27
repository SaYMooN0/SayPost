using ApiShared;
using ApiShared.extensions;
using MediatR;
using SayPostFollowingsService.Api.contracts;
using SayPostFollowingsService.Application.app_users.commands;
using SharedKernel.configs;

namespace SayPostFollowingsService.Api.endpoints;

internal static class SpecificAppUserHandlers
{
    internal static IEndpointRouteBuilder MapSpecificAppUserHandlers(this IEndpointRouteBuilder endpoints) {
        endpoints
            .MapPatch("/follow", HandleUserFollow)
            .WithAuthenticationRequired();
        endpoints
            .MapPatch("/unfollow", HandleUserUnfollow)
            .WithAuthenticationRequired();


        return endpoints;
    }

    private static async Task<IResult> HandleUserFollow(
        HttpContext httpContext, ISender mediator, JwtTokenConfig jwtConfig
    ) {
        var userId = httpContext.GetUserIdFromRoute();

        FollowUserCommand command = new(userId);
        var result = await mediator.Send(command);

        return CustomResults.FromErrOr(result, (data) => Results.Json(
            new PatchFollowingStateResponse(data.Item1, data.Item2)
        ));
    }

    private static async Task<IResult> HandleUserUnfollow(
        HttpContext httpContext, ISender mediator, JwtTokenConfig jwtConfig
    ) {
        var userId = httpContext.GetUserIdFromRoute();

        UnfollowUserCommand command = new(userId);
        var result = await mediator.Send(command);

        return CustomResults.FromErrOr(result, (data) => Results.Json(
            new PatchFollowingStateResponse(data.Item1, data.Item2)
        ));
    }
}