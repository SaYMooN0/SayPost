using ApiShared;
using ApiShared.extensions;
using MediatR;
using SayPostMainService.Api.contracts.published_posts;
using SayPostMainService.Api.extensions;
using SayPostMainService.Application.draft_posts.commands;
using SayPostMainService.Application.published_posts.commands;
using SayPostMainService.Application.published_posts.queries;
using SayPostMainService.Domain.common;
using SharedKernel.common.domain.ids;

namespace SayPostMainService.Api.endpoints;

internal static class SpecificPostHandlers
{
    internal static IEndpointRouteBuilder MapSpecificPostHandlers(this RouteGroupBuilder endpoints) {
        endpoints
            .WithGroupEnsurePostExistsRequired();


        endpoints.MapGet("/", GetPostData);
        endpoints.MapPost("/comment", CommentPost)
            .WithAuthenticationRequired()
            .WithRequestValidation<CommentPostRequest>();


        return endpoints;
    }

    private static async Task<IResult> GetPostData(
        HttpContext httpContext, ISender mediator
    ) {
        PublishedPostId postId = httpContext.GetPublishedPostIdFromRoute();

        GetWithCommentsPostByIdQuery query = new(postId);
        var result = await mediator.Send(query);

        return CustomResults.FromErrOr(result,
            (post) => Results.Json(PostDataResponse.FromPost(post))
        );
    }

    private static async Task<IResult> CommentPost(
        HttpContext httpContext, ISender mediator
    ) {
        AppUserId userId = httpContext.GetAuthenticatedUserId();
        PublishedPostId postId = httpContext.GetPublishedPostIdFromRoute();
        var request = httpContext.GetValidatedRequest<CommentPostRequest>();

        CommentPostCommand command = new(postId, request.Content, userId);
        var result = await mediator.Send(command);

        return CustomResults.FromErrOr(result, (isPinned) => Results.Json(new { IsPostPinned = isPinned }));
    }
}