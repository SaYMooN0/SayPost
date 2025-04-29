using SayPostNotificationService.Domain.app_user_aggregate;

namespace SayPostNotificationService.Api.contracts;

public record NotificationsDataResponse()
{
    public static NotificationsDataResponse FromNotification(Notification notification) => new();
}