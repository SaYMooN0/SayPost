using ApiShared;
using MediatR;
using SayPostMainService.Api.contracts.published_posts;
using SayPostMainService.Application.published_posts.queries;

namespace SayPostMainService.Api.endpoints;

internal static class PostsHandlers
{
    internal static IEndpointRouteBuilder MapPostsHandlers(this RouteGroupBuilder endpoints) {
        endpoints.MapGet("/", ListPosts);

        return endpoints;
    }

    private static async Task<IResult> ListPosts(
        HttpContext httpContext,
        ISender mediator,
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        string[]? includeTags = null
    ) {
        var query = new ListPublishedPostsQuery(dateFrom, dateTo, includeTags ?? []);
        var result = await mediator.Send(query);

        return CustomResults.FromErrOr(result,
            (posts) => Results.Json(ListPublishedPostsResponse.FromPosts(posts))
        );
    }
}