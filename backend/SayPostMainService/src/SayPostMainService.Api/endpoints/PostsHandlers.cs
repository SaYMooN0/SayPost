using ApiShared;
using MediatR;
using SayPostMainService.Api.contracts.published_posts;
using SayPostMainService.Application.published_posts.queries;

namespace SayPostMainService.Api.endpoints;

internal static class PostsHandlers
{
    internal static IEndpointRouteBuilder MapPostsHandlers(this RouteGroupBuilder endpoints) {
        endpoints.MapGet("/", ListPreviews);

        return endpoints;
    }

    private static async Task<IResult> ListPreviews(
        HttpContext httpContext,
        ISender mediator,
        long? dateFrom = null,
        long? dateTo = null,
        string? includeTags = null,
        string? excludeTags = null
    ) {
        ListPostsWithCommentsQuery query = new(
            DateFrom: dateFrom, DateTo: dateTo,
            IncludeTags: includeTags, ExcludeTags: excludeTags
        );
        var result = await mediator.Send(query);

        return CustomResults.FromErrOr(result,
            (posts) => Results.Json(ListPublishedPostPreviewsResponse.FromPosts(posts))
        );
    }
}