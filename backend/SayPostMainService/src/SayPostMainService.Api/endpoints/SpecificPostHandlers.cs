using ApiShared;
using ApiShared.extensions;
using MediatR;
using SayPostMainService.Api.contracts.post_comments;
using SayPostMainService.Api.contracts.published_posts;
using SayPostMainService.Api.extensions;
using SayPostMainService.Application.comments;
using SayPostMainService.Application.comments.commands;
using SayPostMainService.Application.comments.queries;
using SayPostMainService.Application.published_posts.commands;
using SayPostMainService.Application.published_posts.queries;
using SharedKernel.common.domain.ids;
using SharedKernel.configs;

namespace SayPostMainService.Api.endpoints;

internal static class SpecificPostHandlers
{
    internal static IEndpointRouteBuilder MapSpecificPostHandlers(this RouteGroupBuilder endpoints) {
        endpoints
            .WithGroupEnsurePostExistsRequired();

        endpoints.MapGet("/", GetPostData);
        endpoints.MapGet("/comments", GetPostComments);
        endpoints.MapPost("/add-comment", CommentPost)
            .WithAuthenticationRequired()
            .WithRequestValidation<CommentPostRequest>();
        endpoints.MapPatch("/like", LikePost)
            .WithAuthenticationRequired();
        endpoints.MapPatch("/unlike", UnlikePost)
            .WithAuthenticationRequired();


        return endpoints;
    }

    private static async Task<IResult> GetPostData(
        HttpContext httpContext, ISender mediator, JwtTokenConfig jwtConfig
    ) {
        PublishedPostId postId = httpContext.GetPublishedPostIdFromRoute();

        GetPostWithLikesQuery query = new(postId);
        var result = await mediator.Send(query);

        return CustomResults.FromErrOr(result,
            (post) => {
                AppUserId? actorId = null;
                httpContext.ParseUserIdFromJwtToken(jwtConfig).IsSuccess(out actorId);
                return Results.Json(PostDataResponse.FromPost(post, actorId));
            });
    }

    private static async Task<IResult> GetPostComments(
        HttpContext httpContext, ISender mediator, string sortOption = "newest"
    ) {
        PublishedPostId postId = httpContext.GetPublishedPostIdFromRoute();

        CommentsForPostQuery query = new(postId, sortOption);
        var result = await mediator.Send(query);

        return CustomResults.FromErrOr(result, (comments) =>
            Results.Json(ListPostCommentsResponse.FromComments(comments)));
    }

    private static async Task<IResult> CommentPost(
        HttpContext httpContext, ISender mediator
    ) {
        PublishedPostId postId = httpContext.GetPublishedPostIdFromRoute();
        var request = httpContext.GetValidatedRequest<CommentPostRequest>();

        AddCommentToPostCommand command = new(postId, request.Content);
        var result = await mediator.Send(command);

        return CustomResults.FromErrOr(result, (comment) =>
            Results.Json(PostCommentResponse.FromComment(comment)));
    }

    private static async Task<IResult> LikePost(
        HttpContext httpContext, ISender mediator
    ) {
        PublishedPostId postId = httpContext.GetPublishedPostIdFromRoute();

        LikePostCommand command = new(postId);
        var result = await mediator.Send(command);

        return CustomResults.FromErrOr(result, (likesCount) => Results.Ok(new { LikesCount = likesCount }));
    }

    private static async Task<IResult> UnlikePost(
        HttpContext httpContext, ISender mediator
    ) {
        PublishedPostId postId = httpContext.GetPublishedPostIdFromRoute();

        UnlikePostCommand command = new(postId);
        var result = await mediator.Send(command);

        return CustomResults.FromErrOr(result, (likesCount) => Results.Ok(new { LikesCount = likesCount }));
    }
}