using ApiShared;
using ApiShared.extensions;
using MediatR;
using SayPostMainService.Api.contracts.draft_posts;
using SayPostMainService.Application.draft_posts.commands;
using SayPostMainService.Application.draft_posts.queries;
using SayPostMainService.Domain.draft_post_aggregate;
using SharedKernel.common.domain.ids;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Api.endpoints;

internal static class DraftPostHandlers
{
    internal static IEndpointRouteBuilder MapDraftPostHandlers(this IEndpointRouteBuilder endpoints) {
        endpoints.MapGet("/", ListDraftPosts)
            .AuthenticationRequired();
        endpoints.MapPost("/create", CreateDraftPost)
            .AuthenticationRequired();

        return endpoints;
    }

    private static async Task<IResult> ListDraftPosts(
        HttpContext httpContext, ISender mediator, string sortOption = "lastModified"
    ) {
        AppUserId userId = httpContext.GetAuthenticatedUserId();

        var query = new ListDraftPostsForUserQuery(userId, sortOption);
        var result = await mediator.Send(query);

        return CustomResults.FromErrOr(result, (posts) =>
            Results.Json(new { posts = ListDraftPostMainInfoResponse.FromPosts(posts) })
        );
    }

    private static async Task<IResult> CreateDraftPost(
        HttpContext httpContext, ISender mediator
    ) {
        AppUserId userId = httpContext.GetAuthenticatedUserId();

        var command = new CreateNewDraftPostCommand(userId);
        var result= await mediator.Send(command);
        
        return CustomResults.ErrorResponse(ErrFactory.NotImplemented());
    }
}