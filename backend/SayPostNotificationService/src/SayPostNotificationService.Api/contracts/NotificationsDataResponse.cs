using SayPostNotificationService.Domain.app_user_aggregate;

namespace SayPostNotificationService.Api.contracts;

public record NotificationsDataResponse(
    string Id,
    DateTime CreatedAt,
    bool Viewed,
    Dictionary<string, string> TypeSpecificData
)
{
    public static NotificationsDataResponse FromNotification(Notification notification) => new(
        notification.Id.ToString(),
        notification.CreatedAt,
        notification.Viewed,
        notification.TypeSpecificData.ToDictionary()
    );
}