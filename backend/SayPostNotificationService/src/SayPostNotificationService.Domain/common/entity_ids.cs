using SharedKernel.common.domain.ids;

namespace SayPostNotificationService.Domain.common;
public class NotificationId(Guid value) : GuidBasedId(value)
{
    public static NotificationId CreateNew() => new(Guid.CreateVersion7());
}