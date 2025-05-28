using SharedKernel.common.domain.ids;

namespace SayPostNotificationService.Domain.app_user_aggregate.notification_type_specific_data;

public class CommentLeftNotificationSpecificData : BaseNotificationSpecificData
{
    public override NotificationType Type() => NotificationType.CommentLeft;

    public string PostTitle { get;  }
    public AppUserId CommentAuthorId { get; }

    public CommentLeftNotificationSpecificData(string postTitle, AppUserId commentAuthorId) : base() {
        PostTitle = postTitle;
        CommentAuthorId = commentAuthorId;
    }

    public override IEnumerable<object> GetEqualityComponents() => [PostTitle, CommentAuthorId];
    public override Dictionary<string, string> ToDictionary() => new() {
        ["postTitle"] = PostTitle,
        ["commentAuthorId"] = CommentAuthorId.ToString()
    };
}