using ApiShared;
using SayPostNotificationService.Domain.app_user_aggregate;
using SayPostNotificationService.Domain.common;
using SharedKernel.common.errs.utils;

namespace SayPostNotificationService.Api.contracts;

public class ViewNotificationsRequest : IRequestWithValidationNeeded
{
    public string[] NotificationIds { get; init; }

    public RequestValidationResult Validate() {
        if (NotificationIds is null || NotificationIds.Length == 0) {
            return ErrFactory.NoValue("Notification ids is required");
        }

        if (NotificationIds.Length > AppUser.MaxNotificationsToViewAtOnce) {
            return ErrFactory.InvalidData(
                $"Notifications count is too big. Unable to set more than {AppUser.MaxNotificationsToViewAtOnce} notifications as viewed at once",
                $"Current notification count is {NotificationIds.Length}"
            );
        }

        foreach (var id in NotificationIds) {
            if (!Guid.TryParse(id, out _)) {
                return ErrFactory.InvalidData("Notification id is invalid");
            }
        }

        return RequestValidationResult.Success;
    }

    public HashSet<NotificationId> GetParsedNotificationId() => NotificationIds
        .Select(n => new NotificationId(new(n)))
        .ToHashSet();
}