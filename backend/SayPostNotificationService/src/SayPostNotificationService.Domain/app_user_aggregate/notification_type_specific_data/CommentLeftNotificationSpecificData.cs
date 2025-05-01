using SharedKernel.common.domain.ids;

namespace SayPostNotificationService.Domain.app_user_aggregate.notification_type_specific_data;

public class CommentLeftNotificationSpecificData: BaseNotificationSpecificData
{
    public string PostTitle { get;private init; }
    public AppUserId CommentAuthorId { get; private init;}

    public CommentLeftNotificationSpecificData(string postTitle, AppUserId commentAuthorId) {
       PostTitle = postTitle;
       CommentAuthorId = commentAuthorId;
    }

    public override IEnumerable<object> GetEqualityComponents() {
        yield return PostTitle;
        yield return CommentAuthorId;
    }

    public override Dictionary<string, string> ToDictionary() => new() {
        ["PostTitle"] = PostTitle,
        ["CommentAuthorId"] = CommentAuthorId.ToString()
    };
}