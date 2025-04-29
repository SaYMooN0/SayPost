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
    public BaseNotificationSpecificData NotificationSpecificData { get; }
    public bool Viewed { get; private set; }

    private Notification(NotificationId id, DateTime createdAt, BaseNotificationSpecificData notificationSpecificData) {
        Id = id;
        CreatedAt = createdAt;
        NotificationSpecificData = notificationSpecificData;
        Viewed = false;
    }

    public static Notification CreateNewTestPublished(
        IDateTimeProvider dateTimeProvider,
        PublishedPostId postId,
        AppUserId postAuthorId
    ) => new(
        NotificationId.CreateNew(), dateTimeProvider.Now,
        new TestPublishedNotificationSpecificData(postId, postAuthorId)
    );

    public void View()=> Viewed = true;
}