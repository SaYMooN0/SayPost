using ApiShared;
using ApiShared.extensions;
using MediatR;
using SayPostMainService.Api.contracts.app_users;
using SayPostMainService.Api.extensions;
using SayPostMainService.Application.app_users.commands;
using SharedKernel.common.domain.ids;

namespace SayPostMainService.Api.endpoints;

internal static class UserProfileEndpoints
{
    internal static IEndpointRouteBuilder MapUserProfileEndpoints(this RouteGroupBuilder endpoints) {
        endpoints.WithGroupAuthenticationRequired();

        endpoints
            .MapPatch("/update-banner", UpdateUserProfileBanner)
            .WithRequestValidation<UpdateProfileBannerRequest>();

        // endpoints.MapGet("/statistics-visibility", GetUsersStatisticsCardsVisibility);
        return endpoints;
    }

    private static async Task<IResult> UpdateUserProfileBanner(
        HttpContext httpContext, ISender mediator
    ) {
        AppUserId userId = httpContext.GetAuthenticatedUserId();
        var request = httpContext.GetValidatedRequest<UpdateProfileBannerRequest>();

        UpdateUserProfileBannerCommand command = new(
            userId, request.Scale, request.Design, request.DesignVariant, request.ParsedColors
        );
        var result = await mediator.Send(command);

        return CustomResults.FromErrOr(result,
            (banner) => Results.Json(UserProfileBannerResponse.FromProfileBanner(banner))
        );
    }
}