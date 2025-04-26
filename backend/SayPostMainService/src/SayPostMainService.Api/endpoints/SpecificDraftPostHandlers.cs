using ApiShared;
using ApiShared.extensions;
using MediatR;
using SayPostMainService.Api.contracts.draft_posts;
using SayPostMainService.Api.extensions;
using SayPostMainService.Application.draft_posts.commands;
using SayPostMainService.Application.draft_posts.queries;
using SayPostMainService.Domain.common;

namespace SayPostMainService.Api.endpoints;

internal static class SpecificDraftPostHandlers
{
    internal static IEndpointRouteBuilder MapSpecificDraftPostHandlers(this RouteGroupBuilder endpoints) {
        endpoints
            .WithGroupAuthenticationRequired()
            .WithGroupAccessToModifyDraftPostRequired();


        endpoints.MapGet("/", GetDraftPostFullInfo);
        endpoints.MapPatch("/pin", PinDraftPost);
        endpoints.MapPatch("/unpin", UnpinDraftPost);
        endpoints.MapDelete("/delete", DeleteDraftPost);

        endpoints.MapPatch("/update-title", UpdateDraftPostTitle)
            .WithRequestValidation<UpdateDraftPostTitleRequest>();
        endpoints.MapPatch("/update-content", UpdateDraftPostContent)
            .WithRequestValidation<UpdateDraftPostContentRequest>();
        endpoints.MapPatch("/update-tags", UpdateDraftPostTags)
            .WithRequestValidation<UpdateDraftPostTagsRequest>();


        return endpoints;
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

    private static async Task<IResult> PinDraftPost(
        HttpContext httpContext, ISender mediator
    ) {
        DraftPostId postId = httpContext.GetDraftPostIdFromRoute();

        var command = new PinDraftPostCommand(postId);
        var result = await mediator.Send(command);

        return CustomResults.FromErrOr(result, (isPinned) => Results.Json(new { IsPostPinned = isPinned }));
    }
    private static async Task<IResult> UnpinDraftPost(
        HttpContext httpContext, ISender mediator
    ) {
        DraftPostId postId = httpContext.GetDraftPostIdFromRoute();

        var command = new UnpinDraftPostCommand(postId);
        var result = await mediator.Send(command);

        return CustomResults.FromErrOr(result, (isPinned) => Results.Json(new { IsPostPinned = isPinned }));
    }
    private static async Task<IResult> DeleteDraftPost(
        HttpContext httpContext, ISender mediator
    ) {
        DraftPostId postId = httpContext.GetDraftPostIdFromRoute();

        var command = new DeleteDraftPostCommand(postId);
        var result = await mediator.Send(command);

        return CustomResults.FromErrOrNothing(result, () => Results.Ok());
    }

    private static async Task<IResult> UpdateDraftPostTitle(
        HttpContext httpContext, ISender mediator
    ) {
        DraftPostId postId = httpContext.GetDraftPostIdFromRoute();
        var request = httpContext.GetValidatedRequest<UpdateDraftPostTitleRequest>();

        var command = new UpdateDraftPostTitleCommand(postId, request.GetParsedPostTitle());
        var result = await mediator.Send(command);

        return CustomResults.FromErrOr(result,
            (newValues) => Results.Json(new {
                NewTitle = newValues.NewTitle.ToString(),
                NewLastModified = newValues.NewLastModified
            })
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
            (newValues) => Results.Json(new {
                NewPostContent = newValues.NewContent.ToString(),
                NewLastModified = newValues.NewLastModified
            })
        );
    }

    private static async Task<IResult> UpdateDraftPostTags(
        HttpContext httpContext, ISender mediator
    ) {
        DraftPostId postId = httpContext.GetDraftPostIdFromRoute();
        var request = httpContext.GetValidatedRequest<UpdateDraftPostTagsRequest>();

        var command = new UpdateDraftPostTagsCommand(postId, request.GetParsedPostTags());
        var result = await mediator.Send(command);

        return CustomResults.FromErrOr(result,
            (newContent) => Results.Json(new { NewPostContent = newContent.ToString() })
        );
    }
}