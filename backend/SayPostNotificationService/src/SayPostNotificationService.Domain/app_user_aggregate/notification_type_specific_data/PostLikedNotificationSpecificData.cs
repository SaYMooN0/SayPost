using SharedKernel.common.domain.ids;

namespace SayPostNotificationService.Domain.app_user_aggregate.notification_type_specific_data;

public class PostLikedNotificationSpecificData : BaseNotificationSpecificData
{
    public PublishedPostId PostId { get; private init; }
    public AppUserId UserThatLikedId { get; private init; }

    public PostLikedNotificationSpecificData(PublishedPostId postId, AppUserId userThatLikedId) {
        PostId = postId;
        UserThatLikedId = userThatLikedId;
    }

    public override IEnumerable<object> GetEqualityComponents() {
        yield return PostId;
        yield return UserThatLikedId;
    }

    public override Dictionary<string, string> ToDictionary() => new() {
        ["PostId"] = PostId.ToString(),
        ["UserThatLikedId"] = UserThatLikedId.ToString()
    };
}