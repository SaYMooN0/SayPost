using ApiShared;
using MediatR;
using SayPostMainService.Application.post_tags.queries;

namespace SayPostMainService.Api.endpoints;

internal static class PostTagsHandlers
{
    internal static IEndpointRouteBuilder MapPostTagsHandlers(this RouteGroupBuilder endpoints) {
        endpoints.MapGet("/search/{searchVal}", SearchPostTags);

        return endpoints;
    }

    private static async Task<IResult> SearchPostTags(
        HttpContext httpContext, ISender mediator, string searchVal, int? count
    ) {
        var query = new SearchPostTagsQuery(searchVal, count ?? 10 );
        var result = await mediator.Send(query);

        return CustomResults.FromErrOr(result,
            (tags) => Results.Json(new { Tags = tags })
        );
    }
}