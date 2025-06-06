using ApiShared;
using ApiShared.extensions;
using MediatR;
using SayPostMainService.Api.contracts.app_users.statistics;
using SayPostMainService.Application.app_users.queries;
using SharedKernel.common.domain.ids;

namespace SayPostMainService.Api.endpoints;

internal static class AppUserStatisticsHandlers
{
    internal static IEndpointRouteBuilder MapAppUserStatisticsHandlers(this RouteGroupBuilder endpoints) {
        endpoints.MapGet("/", GetAppUserStatistics);
        endpoints.MapGet("/follower-ids", ListUserFollowerIds);
        endpoints.MapGet("/following-ids", ListUserFollowingIds);
        // endpoints.MapGet("/liked-posts", ListUserLikedPosts);
        // endpoints.MapGet("/left-comments", ListUserLeftComments);
        // endpoints.MapGet("/published-posts", ListUserPublishedPosts);

        return endpoints;
    }

    private static async Task<IResult> GetAppUserStatistics(
        HttpContext httpContext, ISender mediator
    ) {
        AppUserId userId = httpContext.GetUserIdFromRoute();

        GetUserProfileStatisticsQuery query = new(userId);
        var result = await mediator.Send(query);

        return CustomResults.FromErrOr(result, (vm) => Results.Json(UserProfileStatisticsResponse.FromVm(vm)));
    }

    private static async Task<IResult> ListUserFollowerIds(
        HttpContext httpContext, ISender mediator
    ) {
        AppUserId userId = httpContext.GetUserIdFromRoute();
    
        ListUserFollowerIdsForStatisticsQuery forStatisticsQuery = new(userId);
        var result = await mediator.Send(forStatisticsQuery);
    
        return CustomResults.FromErrOr(result, (userIds) => Results.Json(
            new { UserIds = userIds.Select(u => u.ToString()) }
        ));
    }
    
    private static async Task<IResult> ListUserFollowingIds(
        HttpContext httpContext, ISender mediator
    ) {
        AppUserId userId = httpContext.GetUserIdFromRoute();
    
        ListUserFollowingIdsForStatisticsQuery query = new(userId);
        var result = await mediator.Send(query);
    
        return CustomResults.FromErrOr(result, (userIds) => Results.Json(
            new { UserIds = userIds.Select(u => u.ToString()) }
        ));
    }
    //
    // private static async Task<IResult> ListUserLikedPosts(
    //     HttpContext httpContext, ISender mediator
    // ) {
    //     AppUserId userId = httpContext.GetUserIdFromRoute();
    //
    //     ListUserLikedPostsQuery query = new(userId);
    //     var result = await mediator.Send(query);
    //
    //     return CustomResults.FromErrOr(result, (posts) => Results.Json(
    //         new ListUserLikedPostsResponse(posts
    //             .Select(p => new PostBriefData(
    //                 p.Id.ToString(),
    //                 p.Title.ToString(),
    //                 p.AuthorId.ToString()
    //             ))
    //             .ToArray()
    //         )
    //     ));
    // }
    //
    // private static async Task<IResult> ListUserLeftComments(
    //     HttpContext httpContext, ISender mediator
    // ) {
    //     AppUserId userId = httpContext.GetUserIdFromRoute();
    //
    //     ListUserLeftCommentsQuery query = new(userId);
    //     var result = await mediator.Send(query);
    //
    //     return CustomResults.FromErrOr(result, (posts) => Results.Json(
    //         new ListUserCommentedPostsResponse(posts
    //             .Select(p => new UserCommentedPostData(
    //                 p.PostId.ToString(),
    //                 p.PostAuthorId.ToString(),
    //                 p.PostTitle.ToString(),
    //                 p.Comments
    //             ))
    //             .ToArray()
    //         )
    //     ));
    // }
    //
    // private static async Task<IResult> ListUserPublishedPosts(
    //     HttpContext httpContext, ISender mediator
    // ) {
    //     AppUserId userId = httpContext.GetUserIdFromRoute();
    //
    //     ListUserPublishedPostsQuery query = new(userId);
    //     var result = await mediator.Send(query);
    //
    //     return Results.Json(ListPublishedPostPreviewsResponse.FromPosts(result)
    //     );
    // }
}