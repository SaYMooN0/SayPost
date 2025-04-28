using SayPostNotificationService.Domain.common;
using SharedKernel.common.domain;
using SharedKernel.date_time_provider;

namespace SayPostNotificationService.Domain.notification_aggregate;

public class Notification : AggregateRoot<NotificationId>
{
    private Notification() { }
    public DateTime CreatedAt { get; }

    private Notification(DateTime createdAt) {
        CreatedAt = createdAt;
    }

    public static Notification CreateNew(IDateTimeProvider dateTimeProvider) =>
        new(dateTimeProvider.Now) {
            Id = NotificationId.CreateNew(),
        };
}