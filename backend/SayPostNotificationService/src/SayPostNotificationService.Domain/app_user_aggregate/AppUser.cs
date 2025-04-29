using SayPostNotificationService.Domain.common;
using SharedKernel.common.domain;
using SharedKernel.common.domain.ids;

namespace SayPostNotificationService.Domain.app_user_aggregate;

public class AppUser : AggregateRoot<AppUserId>
{
    private AppUser() { }
    private ICollection<Notification> _notifications { get; }

    public AppUser(AppUserId id) {
        Id = id;
        _notifications = [];
    }

    public Notification[] GetSortedNotifications() => _notifications
        .OrderBy(n => n.CreatedAt)
        .ToArray();

    public void AddNotification(Notification notification) =>
        _notifications.Add(notification);

    public void ViewNotifications(HashSet<NotificationId> notificationIds) {
        foreach (var nId in notificationIds) {
            Notification? notification = _notifications.FirstOrDefault(n => n.Id == nId);
            if (notification is not null) {
                notification.View();
            }
        }
    }
}