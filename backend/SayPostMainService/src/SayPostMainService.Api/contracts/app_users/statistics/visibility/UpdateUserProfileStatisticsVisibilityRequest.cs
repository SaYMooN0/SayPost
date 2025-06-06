using ApiShared;

namespace SayPostMainService.Api.contracts.app_users.statistics.visibility;

public class UpdateUserProfileStatisticsVisibilityRequest : IRequestWithValidationNeeded
{
    public bool PublishedPostsOnlyForFollowers { get; init; }
    public bool FollowersOnlyForFollowers{ get; init; }
    public bool FollowingOnlyForFollowers{ get; init; }
    public bool LikedPostsOnlyForFollowers{ get; init; }
    public  bool LeftCommentsOnlyForFollowers{ get; init; }
    public RequestValidationResult Validate() => RequestValidationResult.Success;
}