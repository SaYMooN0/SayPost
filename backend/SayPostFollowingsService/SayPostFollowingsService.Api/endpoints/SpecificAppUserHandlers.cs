using ApiShared;
using ApiShared.extensions;
using MediatR;
using SayPostFollowingsService.Application.app_users.commands;
using SharedKernel.configs;

namespace SayPostFollowingsService.Api.endpoints;

internal static class SpecificAppUserHandlers
{
    internal static IEndpointRouteBuilder MapSpecificAppUserHandlers(this IEndpointRouteBuilder endpoints) {
        endpoints
            .MapPost("/follow", HandleUserFollow)
            .WithAuthenticationRequired();
        endpoints
            .MapPost("/unfollow", HandleUserUnfollow)
            .WithAuthenticationRequired();


        return endpoints;
    }

    private static async Task<IResult> HandleUserFollow(
        HttpContext httpContext, ISender mediator, JwtTokenConfig jwtConfig
    ) {
        var userId = httpContext.GetUserIdFromRoute();

        FollowUserCommand command = new(userId);
        var result = await mediator.Send(command);

        return CustomResults.FromErrOr(result, (isFollowed) => Results.Json(new { isFollowed = isFollowed }));
    }

    private static async Task<IResult> HandleUserUnfollow(
        HttpContext httpContext, ISender mediator, JwtTokenConfig jwtConfig
    ) {
        var userId = httpContext.GetUserIdFromRoute();

        UnfollowUserCommand command = new(userId);
        var result = await mediator.Send(command);

        return CustomResults.FromErrOr(result, (isFollowed) => Results.Json(new { isFollowed = isFollowed }));
    }
}