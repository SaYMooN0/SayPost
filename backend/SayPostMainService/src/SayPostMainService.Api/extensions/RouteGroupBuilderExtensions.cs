using SayPostMainService.Api.endpoints_filters;

namespace SayPostMainService.Api.extensions;

public static class RouteGroupBuilderExtensions
{
    public static RouteGroupBuilder WithGroupAccessToModifyDraftPostRequired(this RouteGroupBuilder builder) {
        return builder.AddEndpointFilter<AccessToModifyDraftPostRequiredEndpointFilter>();
    }
}