using SayPostFollowingsService.Domain.app_user_aggregate.events;
using SharedKernel.common.domain;
using SharedKernel.common.domain.ids;

namespace SayPostFollowingsService.Domain.app_user_aggregate;

public class AppUser : AggregateRoot<AppUserId>
{
    private AppUser() { }
    private HashSet<AppUserId> _followerIds { get; }
    private HashSet<AppUserId> _followingIds { get; }

    public AppUser(AppUserId id) {
        Id = id;
        _followerIds = [];
        _followingIds = [];
    }

    public bool AddFollower(AppUserId newFollowerId) {
        var changeMade = _followerIds.Add(newFollowerId);
        if (changeMade) {
            AddDomainEvent(new UserGotNewFollowingEvent(newFollowerId, FollowingId: Id));
        }

        return changeMade;
    }

    public bool RemoveFollower(AppUserId followerToRemoveId) {
        var changeMade = _followerIds.Remove(followerToRemoveId);
        if (changeMade) {
            AddDomainEvent(new UserLostFollowingEvent(followerToRemoveId, FollowingId: Id));
        }

        return changeMade;
    }

    public bool IsFollowedBy(AppUserId appUserId) => _followerIds.Contains(appUserId);
    public int FollowersCount() => _followerIds.Count;
    public bool AddFollowing(AppUserId followingUserId) => _followingIds.Add(followingUserId);
    public bool RemoveFollowing(AppUserId followingUserId) => _followingIds.Remove(followingUserId);
}