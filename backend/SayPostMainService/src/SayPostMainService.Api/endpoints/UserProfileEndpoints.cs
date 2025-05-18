using ApiShared;
using ApiShared.extensions;
using MediatR;
using SayPostMainService.Api.contracts.app_users;
using SayPostMainService.Api.extensions;
using SayPostMainService.Application.app_users.queries;
using SharedKernel.common.domain.ids;

namespace SayPostMainService.Api.endpoints;

internal static class UserProfileEndpoints
{
    internal static IEndpointRouteBuilder MapUserProfileEndpoints(this RouteGroupBuilder endpoints) {
        endpoints.WithGroupAuthenticationRequired();
        
        endpoints
            .MapPut("/update-profile-banner", UpdateUserProfileBanner)
            .WithRequestValidation<UpdateProfileBannerRequest>();
        return endpoints;
    }

    private static async Task<IResult> UpdateUserProfileBanner(
        HttpContext httpContext, ISender mediator
    ) {
        AppUserId userId = httpContext.GetUserIdFromRoute();
        var request = httpContext.GetValidatedRequest<UpdateProfileBannerRequest>();

        UpdateUserProfileBannerCommand command = new(userId);
        var result = await mediator.Send(command);

        return CustomResults.FromErrOr(result,
            (banner) => Results.Json(UserProfileBannerResponseData.FromProfileBanner(banner))
        );
    }
}