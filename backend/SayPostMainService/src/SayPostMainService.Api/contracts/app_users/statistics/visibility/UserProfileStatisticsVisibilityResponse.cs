using SayPostMainService.Domain.app_user_aggregate;

namespace SayPostMainService.Api.contracts.app_users.statistics.visibility;

public record class UserProfileStatisticsVisibilityResponse(
    bool PublishedPostsOnlyForFollowers,
    bool FollowersOnlyForFollowers,
    bool FollowingOnlyForFollowers,
    bool LikedPostsOnlyForFollowers,
    bool LeftCommentsOnlyForFollowers
)
{
    public static UserProfileStatisticsVisibilityResponse Create(StatisticsVisibilitySettings settings) => new(
        settings.PublishedPostsOnlyForFollowers,
        settings.FollowersOnlyForFollowers,
        settings.FollowingOnlyForFollowers,
        settings.LikedPostsOnlyForFollowers,
        settings.LeftCommentsOnlyForFollowers
    );
}