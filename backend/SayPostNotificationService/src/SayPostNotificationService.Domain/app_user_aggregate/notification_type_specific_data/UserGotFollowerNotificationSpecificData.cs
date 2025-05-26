using SharedKernel.common.domain.ids;

namespace SayPostNotificationService.Domain.app_user_aggregate.notification_type_specific_data;

public class UserGotFollowerNotificationSpecificData : BaseNotificationSpecificData
{
    public override NotificationType Type() => NotificationType.UserGotFollower;

    public AppUserId FollowerId { get; }

    public UserGotFollowerNotificationSpecificData(AppUserId followerId) : base() {
        FollowerId = followerId;
    }


    public override IEnumerable<object> GetEqualityComponents() => [FollowerId];

    public override Dictionary<string, string> ToDictionary() => new() {
        ["FollowerId"] = FollowerId.ToString(),
    };
}