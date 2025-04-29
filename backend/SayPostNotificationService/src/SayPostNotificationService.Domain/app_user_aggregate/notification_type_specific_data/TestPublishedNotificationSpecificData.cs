using SharedKernel.common.domain.ids;

namespace SayPostNotificationService.Domain.app_user_aggregate.notification_type_specific_data;

public class TestPublishedNotificationSpecificData : BaseNotificationSpecificData
{
    private PublishedPostId PostId { get; }
    private AppUserId PostAuthorId { get; }

    public TestPublishedNotificationSpecificData(PublishedPostId postId, AppUserId postAuthorId) {
        PostId = postId;
        PostAuthorId = postAuthorId;
    }

    public override IEnumerable<object> GetEqualityComponents() {
        yield return PostId;
        yield return PostAuthorId;
    }
}