using ApiShared;
using ApiShared.extensions;
using MediatR;
using SayPostMainService.Api.contracts.app_users;
using SayPostMainService.Api.contracts.app_users.statistics.visibility;
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

        endpoints.MapGet("/statistics-visibility", GetUsersStatisticsVisibility);
        endpoints.MapPatch("/update-statistics-visibility", UpdateStatisticsVisibility)
            .WithRequestValidation<UpdateUserProfileStatisticsVisibilityRequest>();
        return endpoints;
    }

    private static async Task<IResult> UpdateUserProfileBanner(
        HttpContext httpContext, ISender mediator
    ) {
        var request = httpContext.GetValidatedRequest<UpdateProfileBannerRequest>();

        UpdateUserProfileBannerCommand command = new(
            request.Scale, request.Design, request.DesignVariant, request.ParsedColors
        );
        var result = await mediator.Send(command);

        return CustomResults.FromErrOr(result,
            (banner) => Results.Json(UserProfileBannerResponse.FromProfileBanner(banner))
        );
    }

    private static async Task<IResult> GetUsersStatisticsVisibility(
        HttpContext httpContext, ISender mediator
    ) {
        var request = httpContext.GetValidatedRequest<UpdateProfileBannerRequest>();

        UpdateUserProfileBannerCommand command = new(
            request.Scale, request.Design, request.DesignVariant, request.ParsedColors
        );
        var result = await mediator.Send(command);

        return CustomResults.FromErrOr(result,
            (banner) => Results.Json(UserProfileBannerResponse.FromProfileBanner(banner))
        );
    }

    private static async Task<IResult> UpdateStatisticsVisibility(
        HttpContext httpContext, ISender mediator
    ) {
        var request = httpContext.GetValidatedRequest<UpdateUserProfileStatisticsVisibilityRequest>();

        UpdateUserStatisticsVisibilityCommand command = new(request.ParseToSettings());
        var result = await mediator.Send(command);

        return CustomResults.FromErrOr(result,
            (banner) => Results.Json(UserProfileBannerResponse.FromProfileBanner(banner))
        );
    }
}