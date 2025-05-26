using SayPostNotificationService.Domain.app_user_aggregate;

namespace SayPostNotificationService.Api.contracts;

public record NotificationsDataResponse(
    string Id,
    bool Viewed,
    DateTime DateTime,
    NotificationType Type,
    Dictionary<string, string> TypeSpecificData
)
{
    public static NotificationsDataResponse FromNotification(Notification notification) => new(
        notification.Id.ToString(),
        notification.Viewed,
        notification.CreatedAt,
        notification.TypeSpecificData.Type(),
        notification.TypeSpecificData.ToDictionary()
    );
}