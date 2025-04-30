using SharedKernel.common.domain.ids;

namespace SayPostNotificationService.Domain.app_user_aggregate.notification_type_specific_data;

public class TestPublishedNotificationSpecificData : BaseNotificationSpecificData
{
    public PublishedPostId PostId { get;private init; }
    public AppUserId PostAuthorId { get; private init;}

    public TestPublishedNotificationSpecificData(PublishedPostId postId, AppUserId postAuthorId) {
        PostId = postId;
        PostAuthorId = postAuthorId;
    }

    public override IEnumerable<object> GetEqualityComponents() {
        yield return PostId;
        yield return PostAuthorId;
    }

    public override Dictionary<string, string> ToDictionary() => new() {
        ["PostId"] = PostId.ToString(),
        ["PostAuthorId"] = PostAuthorId.ToString()
    };
}