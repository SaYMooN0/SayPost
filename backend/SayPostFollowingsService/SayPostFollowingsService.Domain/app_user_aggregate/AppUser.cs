using SharedKernel.common.domain;
using SharedKernel.common.domain.ids;
using SharedKernel.common.errs;

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

    public bool AddFollower(AppUserId newFollowerId) => _followerIds.Add(newFollowerId);

    public bool RemoveFollower(AppUserId followerToRemoveId) => _followingIds.Remove(followerToRemoveId);

    public bool IsFollowedBy(AppUserId appUserId) => _followerIds.Contains(appUserId);
}