using System.Collections.Immutable;
using SharedKernel.common.domain;
using SharedKernel.common.domain.ids;
using SharedKernel.common.errs;

namespace FollowingsQueryLib.entities;

public class AppUserWithFollowingsData : AggregateRoot<AppUserId>
{
    private AppUserWithFollowingsData() { }
    public ImmutableHashSet<AppUserId> FollowerIds { get; }
    public ImmutableHashSet<AppUserId> FollowingIds { get; }
    public bool IsFollowedBy(AppUserId followerId) => FollowerIds.Contains(followerId);
    public int FollowersCount => FollowerIds.Count;
    public int FollowingsCount => FollowingIds.Count;
}