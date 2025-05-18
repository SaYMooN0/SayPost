using ApiShared;
using MediatR;
using SayPostMainService.Api.contracts.app_users;
using SayPostMainService.Api.extensions;
using SayPostMainService.Application.app_users.queries;
using SharedKernel.common.domain.ids;

namespace SayPostMainService.Api.endpoints;

internal static class SpecificAppUserHandlers
{
    internal static IEndpointRouteBuilder MapSpecificAppUserHandlers(this RouteGroupBuilder endpoints) {
        endpoints.MapGet("/profile-data", GetAppUserProfileData);
        
        return endpoints;
    }

    private static async Task<IResult> GetAppUserProfileData(
        HttpContext httpContext, ISender mediator
    ) {
        AppUserId userId = httpContext.GetUserIdFromRoute();

        GetUserWithProfileBannerQuery query = new(userId);
        var result = await mediator.Send(query);

        return CustomResults.FromErrOr(result,
            (user) => Results.Json(UserProfileDataResponse.FromUser(user))
        );
    } 
}