using SayPostMainService.Domain.app_user_aggregate;

namespace SayPostMainService.Api.contracts.app_users;

public record UserProfileStatisticsResponse(
    int PublishedPostsCount,
    int FollowersCount,
    int FollowingsCount,
    int CommentsLeftCount,
    int LikedPostsCount
)
{
    public static UserProfileStatisticsResponse Create(AppUser user, int followersCount, int followingsCount) => new(
        user.PublishedPostsCount,
        followersCount,
        followingsCount,
        user.LeftCommentsCount,
        user.LikedPostsCount
    );
}