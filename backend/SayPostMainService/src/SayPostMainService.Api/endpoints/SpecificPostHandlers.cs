using ApiShared;
using ApiShared.extensions;
using MediatR;
using SayPostMainService.Api.contracts.published_posts;
using SayPostMainService.Api.extensions;
using SayPostMainService.Application.comments;
using SayPostMainService.Application.draft_posts.commands;
using SayPostMainService.Application.published_posts.commands;
using SayPostMainService.Application.published_posts.queries;
using SayPostMainService.Domain.common;
using SharedKernel.common.domain.ids;
using SharedKernel.configs;

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

    private static async Task<IResult> CommentPost(
        HttpContext httpContext, ISender mediator
    ) {
        AppUserId userId = httpContext.GetAuthenticatedUserId();
        PublishedPostId postId = httpContext.GetPublishedPostIdFromRoute();
        var request = httpContext.GetValidatedRequest<CommentPostRequest>();

        CommentPostCommand command = new(postId, request.Content);
        var result = await mediator.Send(command);

        return CustomResults.FromErrOr(result, (comment) => Results.Json(PostCommentData.FromComment(comment)));
    }
}