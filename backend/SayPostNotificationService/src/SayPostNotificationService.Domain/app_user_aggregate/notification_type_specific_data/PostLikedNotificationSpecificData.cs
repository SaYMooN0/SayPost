using SharedKernel.common.domain.ids;

namespace SayPostNotificationService.Domain.app_user_aggregate.notification_type_specific_data;

public class PostLikedNotificationSpecificData : BaseNotificationSpecificData
{
    public override NotificationType Type() => NotificationType.PostLiked;

    public PublishedPostId PostId { get; }
    public AppUserId UserThatLikedId { get;  }

    public PostLikedNotificationSpecificData(PublishedPostId postId, AppUserId userThatLikedId) : base() {
        PostId = postId;
        UserThatLikedId = userThatLikedId;
    }

    public override IEnumerable<object> GetEqualityComponents() => [PostId, UserThatLikedId];

    public override Dictionary<string, string> ToDictionary() => new() {
        ["postId"] = PostId.ToString(),
        ["userThatLikedId"] = UserThatLikedId.ToString()
    };
}