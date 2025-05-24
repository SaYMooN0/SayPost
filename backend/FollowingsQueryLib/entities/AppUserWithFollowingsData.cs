using System.Collections.Immutable;
using SharedKernel.common.domain;
using SharedKernel.common.domain.ids;

namespace FollowingsQueryLib.entities;

public class AppUserWithFollowingsData : AggregateRoot<AppUserId>
{
    private AppUserWithFollowingsData() { }
    private ImmutableHashSet<AppUserId> _followerIds { get; }
    private ImmutableHashSet<AppUserId> _followingIds { get; }
    public bool IsFollowedBy(AppUserId followerId) => _followerIds.Contains(followerId);
    public int FollowersCount => _followerIds.Count;
    public int FollowingsCount => _followingIds.Count;
}