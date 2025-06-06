namespace SayPostMainService.Domain.app_user_aggregate;

public record class StatisticsVisibilitySettings(
    bool PublishedPostsOnlyForFollowers,
    bool FollowersOnlyForFollowers,
    bool FollowingOnlyForFollowers,
    bool LikedPostsOnlyForFollowers,
    bool LeftCommentsOnlyForFollowers
)
{
    public static StatisticsVisibilitySettings Default => new(
        false,
        false,
        false,
        false,
        false
    );
}