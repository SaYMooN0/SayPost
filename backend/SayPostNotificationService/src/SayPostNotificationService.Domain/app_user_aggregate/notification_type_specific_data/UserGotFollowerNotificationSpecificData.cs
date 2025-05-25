using SharedKernel.common.domain.ids;

namespace SayPostNotificationService.Domain.app_user_aggregate.notification_type_specific_data;

public class UserGotFollowerNotificationSpecificData : BaseNotificationSpecificData
{
    public UserGotFollowerNotificationSpecificData(AppUserId followerId) {
        FollowerId = followerId;
    }

    public AppUserId FollowerId { get; private init; }


    public override IEnumerable<object> GetEqualityComponents() {
        yield return FollowerId;
    }

    public override Dictionary<string, string> ToDictionary() => new() {
        ["FollowerId"] = FollowerId.ToString(),
    };
}