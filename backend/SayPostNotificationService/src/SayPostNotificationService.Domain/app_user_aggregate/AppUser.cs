using SayPostNotificationService.Domain.common;
using SharedKernel.common.domain;
using SharedKernel.common.domain.ids;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostNotificationService.Domain.app_user_aggregate;

public class AppUser : AggregateRoot<AppUserId>
{
    private AppUser() { }
    private ICollection<Notification> _notifications { get; }

    public AppUser(AppUserId id) {
        Id = id;
        _notifications = [];
    }

    public IReadOnlyCollection<Notification> Notifications => _notifications.ToList();

    public void AddNotification(Notification notification) =>
        _notifications.Add(notification);

    public const int MaxNotificationsToViewAtOnce = 120;

    public ErrOrNothing ViewNotifications(HashSet<NotificationId> notificationIds) {
        if (notificationIds.Count > MaxNotificationsToViewAtOnce) {
            return ErrFactory.InvalidData(
                $"Cannot view more than {MaxNotificationsToViewAtOnce} notifications at once"
            );
        }

        foreach (var nId in notificationIds) {
            Notification? notification = _notifications.FirstOrDefault(n => n.Id == nId);
            if (notification is not null) {
                notification.View();
            }
        }

        return ErrOrNothing.Nothing;
    }
}