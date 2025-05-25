using SayPostNotificationService.Domain.app_user_aggregate.notification_type_specific_data;
using SayPostNotificationService.Domain.common;
using SharedKernel.common.domain.entity;
using SharedKernel.common.domain.ids;
using SharedKernel.date_time_provider;

namespace SayPostNotificationService.Domain.app_user_aggregate;

public class Notification : Entity<NotificationId> 
{
    private Notification() { }
    public DateTime CreatedAt { get; }
    public BaseNotificationSpecificData TypeSpecificData { get; }
    public bool Viewed { get; private set; }

    private Notification(NotificationId id, DateTime createdAt, BaseNotificationSpecificData typeSpecificData) {
        Id = id;
        CreatedAt = createdAt;
        TypeSpecificData = typeSpecificData;
        Viewed = false;
    }

    public void View() => Viewed = true;

    public static Notification CreateNewTestPublished(
        IDateTimeProvider dateTimeProvider,
        PublishedPostId postId,
        AppUserId postAuthorId
    ) => new(
        NotificationId.CreateNew(), dateTimeProvider.Now,
        new TestPublishedNotificationSpecificData(postId, postAuthorId)
    );

    public static Notification CreateNewCommentLeft(
        IDateTimeProvider dateTimeProvider,
        string postTitle,
        AppUserId commentAuthorId
    ) => new(NotificationId.CreateNew(), dateTimeProvider.Now,
        new CommentLeftNotificationSpecificData(postTitle, commentAuthorId)
    );

    public static Notification CreateNewPostLiked(
        IDateTimeProvider dateTimeProvider,
        PublishedPostId postId,
        AppUserId userThatLikedId
    ) => new(NotificationId.CreateNew(), dateTimeProvider.Now,
        new PostLikedNotificationSpecificData(postId, userThatLikedId)
    );

    public static Notification CreateNewUserGotFollower(
        IDateTimeProvider dateTimeProvider,
        AppUserId followerId
    ) => new(NotificationId.CreateNew(), dateTimeProvider.Now,
        new UserGotFollowerNotificationSpecificData(followerId)
    );
}