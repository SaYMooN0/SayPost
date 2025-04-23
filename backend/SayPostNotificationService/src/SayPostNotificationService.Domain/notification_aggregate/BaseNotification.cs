using SayPostNotificationService.Domain.common;
using SharedKernel.common.domain;

namespace SayPostNotificationService.Domain.notification_aggregate;

public abstract class BaseNotification : AggregateRoot<NotificationId>
{
    protected BaseNotification() { }
    public DateTime CreatedAt { get; }

    protected BaseNotification(DateTime createdAt) {
        CreatedAt = createdAt;
    }
}