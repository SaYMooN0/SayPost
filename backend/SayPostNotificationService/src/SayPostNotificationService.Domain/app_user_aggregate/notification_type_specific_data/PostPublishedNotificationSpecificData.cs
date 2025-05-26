using SharedKernel.common.domain.ids;

namespace SayPostNotificationService.Domain.app_user_aggregate.notification_type_specific_data;

public class PostPublishedNotificationSpecificData : BaseNotificationSpecificData
{
    public override NotificationType Type() => NotificationType.PostPublished;

    public PublishedPostId PostId { get; }
    public AppUserId PostAuthorId { get; }

    public PostPublishedNotificationSpecificData(PublishedPostId postId, AppUserId postAuthorId) : base() {
        PostId = postId;
        PostAuthorId = postAuthorId;
    }

    public override IEnumerable<object> GetEqualityComponents() => [PostId, PostAuthorId];

    public override Dictionary<string, string> ToDictionary() => new() {
        ["PostId"] = PostId.ToString(),
        ["PostAuthorId"] = PostAuthorId.ToString()
    };
}