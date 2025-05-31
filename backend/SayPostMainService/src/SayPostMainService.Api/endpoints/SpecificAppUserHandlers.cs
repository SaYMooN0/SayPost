using ApiShared;
using ApiShared.extensions;
using MediatR;
using SayPostMainService.Api.contracts.app_users;
using SayPostMainService.Api.contracts.published_posts;
using SayPostMainService.Application.app_users.queries;
using SharedKernel.common.domain.ids;

namespace SayPostMainService.Api.endpoints;

internal static class SpecificAppUserHandlers
{
    internal static IEndpointRouteBuilder MapSpecificAppUserHandlers(this RouteGroupBuilder endpoints) {
        endpoints.MapGet("/profile-data", GetAppUserProfileData);
        endpoints.MapGet("/list-follower-ids", ListUserFollowerIds);
        endpoints.MapGet("/list-following-ids", ListUserFollowingIds);
        endpoints.MapGet("/list-liked-posts", ListUserLikedPosts);
        endpoints.MapGet("/list-left-comments", ListUserLeftComments);
        endpoints.MapGet("/list-published-posts", ListUserPublishedPosts);

        return endpoints;
    }

    private static async Task<IResult> GetAppUserProfileData(
        HttpContext httpContext, ISender mediator
    ) {
        AppUserId userId = httpContext.GetUserIdFromRoute();

        GetUserFullProfileDataQuery query = new(userId);
        var result = await mediator.Send(query);

        return CustomResults.FromErrOr(result, (vm) => Results.Json(UserProfileDataResponse.Create(vm)));
    }

    private static async Task<IResult> ListUserFollowerIds(
        HttpContext httpContext, ISender mediator
    ) {
        AppUserId userId = httpContext.GetUserIdFromRoute();

        ListUserFollowerIdsQuery query = new(userId);
        var result = await mediator.Send(query);

        return CustomResults.FromErrOr(result, (userIds) => Results.Json(
            new { UserIds = userIds }
        ));
    }

    private static async Task<IResult> ListUserFollowingIds(
        HttpContext httpContext, ISender mediator
    ) {
        AppUserId userId = httpContext.GetUserIdFromRoute();

        ListUserFollowingIdsQuery query = new(userId);
        var result = await mediator.Send(query);

        return CustomResults.FromErrOr(result, (userIds) => Results.Json(
            new { UserIds = userIds }
        ));
    }

    private static async Task<IResult> ListUserLikedPosts(
        HttpContext httpContext, ISender mediator
    ) {
        AppUserId userId = httpContext.GetUserIdFromRoute();

        ListUserLikedPostsQuery query = new(userId);
        var result = await mediator.Send(query);

        return CustomResults.FromErrOr(result, (posts) => Results.Json(
            new ListUserLikedPostsResponse(posts
                .Select(p => new PostBriefData(p.Title.ToString(), p.AuthorId.ToString()))
                .ToArray()
            )
        ));
    }

    private static async Task<IResult> ListUserLeftComments(
        HttpContext httpContext, ISender mediator
    ) {
        AppUserId userId = httpContext.GetUserIdFromRoute();

        ListUserLeftCommentsQuery query = new(userId);
        var result = await mediator.Send(query);

        return CustomResults.FromErrOr(result, (posts) => Results.Json(
            new ListUserCommentedPostsResponse(posts
                .Select(p => new UserCommentedPostData(
                    p.PostId.ToString(),
                    p.PostAuthorId.ToString(),
                    p.PostTitle.ToString(),
                    p.Comments
                ))
                .ToArray()
            )
        ));
    }

    private static async Task<IResult> ListUserPublishedPosts(
        HttpContext httpContext, ISender mediator
    ) {
        AppUserId userId = httpContext.GetUserIdFromRoute();

        ListUserPublishedPostsQuery query = new(userId);
        var result = await mediator.Send(query);

        return Results.Json(ListPublishedPostPreviewsResponse.FromPosts(result)
        );
    }
}