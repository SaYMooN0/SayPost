using ApiShared;
using ApiShared.extensions;
using MediatR;
using SayPostMainService.Api.contracts.draft_posts;
using SayPostMainService.Application.draft_posts.commands;
using SayPostMainService.Application.draft_posts.queries;
using SharedKernel.common.domain.ids;

namespace SayPostMainService.Api.endpoints;

internal static class DraftPostsHandlers
{
    internal static IEndpointRouteBuilder MapDraftPostsHandlers(this RouteGroupBuilder endpoints) {
        endpoints
            .WithGroupAuthenticationRequired();


        endpoints.MapGet("/", ListDraftPosts)
            .WithAuthenticationRequired();
        endpoints.MapPost("/create", CreateDraftPost)
            .WithAuthenticationRequired();


        return endpoints;
    }

    private static async Task<IResult> ListDraftPosts(
        HttpContext httpContext, ISender mediator, string sortOption = "lastModified", bool putPinnedOnTop = true
    ) {
        AppUserId userId = httpContext.GetAuthenticatedUserId();

        var query = new ListDraftPostsForUserQuery(userId, sortOption, putPinnedOnTop);
        var result = await mediator.Send(query);

        return CustomResults.FromErrOr(result,
            (posts) => Results.Json(ListDraftPostMainInfoResponse.FromPosts(posts))
        );
    }

    private static async Task<IResult> CreateDraftPost(
        HttpContext httpContext, ISender mediator
    ) {
        AppUserId userId = httpContext.GetAuthenticatedUserId();

        var command = new CreateNewDraftPostCommand(userId);
        var result = await mediator.Send(command);

        return CustomResults.FromErrOr(result,
            (draftPost) => Results.Json(DraftPostFullInfoResponse.FromDraftPost(draftPost))
        );
    }
}