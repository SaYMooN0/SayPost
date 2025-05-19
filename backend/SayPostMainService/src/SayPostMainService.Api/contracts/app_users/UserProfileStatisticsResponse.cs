using SayPostMainService.Domain.app_user_aggregate;

namespace SayPostMainService.Api.contracts.app_users;

public record UserProfileStatisticsResponse(
    int PublishedPostsCount,
    int FollowersCount,
    int CommentsLeftCount,
    int LikedPostsCount
)
{
    public static UserProfileStatisticsResponse Create(AppUser user, int followersCount) => new(
        user.PublishedPostsCount,
        followersCount,
        user.LeftCommentsCount,
        user.LikedPostsCount
    );
}