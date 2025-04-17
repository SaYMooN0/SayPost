using ApiShared.endpoints_filters;
using SayPostMainService.Api.endpoints_filters;

namespace SayPostMainService.Api.extensions;

public static class EndpointRouteBuilderExtensions
{
    public static RouteHandlerBuilder WithAccessToModifyDraftPostRequired(this RouteHandlerBuilder builder) {
        return builder.AddEndpointFilter<AccessToModifyDraftPostRequiredEndpointFilter>();
    }
}