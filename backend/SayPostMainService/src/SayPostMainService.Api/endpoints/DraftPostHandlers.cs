using ApiShared;
using ApiShared.extensions;
using MediatR;
using SayPostMainService.Api.contracts.draft_posts;
using SayPostMainService.Api.extensions;
using SayPostMainService.Application.draft_posts.commands;
using SayPostMainService.Application.draft_posts.queries;
using SayPostMainService.Domain.common;
using SharedKernel.common.domain.ids;

namespace SayPostMainService.Api.endpoints;

internal static class DraftPostHandlers
{
    internal static IEndpointRouteBuilder MapDraftPostHandlers(this IEndpointRouteBuilder endpoints) {
        endpoints.MapGet("/", ListDraftPosts)
            .WithAuthenticationRequired();
        endpoints.MapPost("/create", CreateDraftPost)
            .WithAuthenticationRequired();

        endpoints.MapGet("/{postId}/", GetDraftPostFullInfo)
            .WithAuthenticationRequired()
            .WithAccessToModifyDraftPostRequired();
        endpoints.MapPatch("/{postId}/updateTitle", UpdateDraftPostTitle)
            .WithAuthenticationRequired()
            .WithRequestValidation<UpdateDraftPostTitleRequest>()
            .WithAccessToModifyDraftPostRequired();
        endpoints.MapPatch("/{postId}/updateContent", UpdateDraftPostContent)
            .WithAuthenticationRequired()
            .WithRequestValidation<UpdateDraftPostContentRequest>()
            .WithAccessToModifyDraftPostRequired();

        return endpoints;
    }

    private static async Task<IResult> ListDraftPosts(
        HttpContext httpContext, ISender mediator, string sortOption = "lastModified"
    ) {
        AppUserId userId = httpContext.GetAuthenticatedUserId();

        var query = new ListDraftPostsForUserQuery(userId, sortOption);
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

    private static async Task<IResult> GetDraftPostFullInfo(
        HttpContext httpContext, ISender mediator
    ) {
        DraftPostId postId = httpContext.GetDraftPostIdFromRoute();

        var query = new GetDraftPostByIdQuery(postId);
        var result = await mediator.Send(query);

        return CustomResults.FromErrOr(result,
            (draftPost) => Results.Json(DraftPostFullInfoResponse.FromDraftPost(draftPost))
        );
    }


    private static async Task<IResult> UpdateDraftPostTitle(
        HttpContext httpContext, ISender mediator
    ) {
        DraftPostId postId = httpContext.GetDraftPostIdFromRoute();
        var request = httpContext.GetValidatedRequest<UpdateDraftPostTitleRequest>();

        var command = new UpdateDraftPostTitleCommand(postId, request.GetParsedPostTitle());
        var result = await mediator.Send(command);

        return CustomResults.FromErrOr(result,
            (newTitle) => Results.Json(new { NewPostTitle = newTitle.ToString() })
        );
    }

    private static async Task<IResult> UpdateDraftPostContent(
        HttpContext httpContext, ISender mediator
    ) {
        DraftPostId postId = httpContext.GetDraftPostIdFromRoute();
        var request = httpContext.GetValidatedRequest<UpdateDraftPostContentRequest>();

        var command = new UpdateDraftPostContentCommand(postId, request.GetParsedPostContent());
        var result = await mediator.Send(command);

        return CustomResults.FromErrOr(result,
            (newContent) => Results.Json(new { NewPostContent = newContent.ToString() })
        );
    }
}