using System.Collections.Immutable;
using SharedKernel.common.domain;
using SharedKernel.common.domain.ids;

namespace FollowingsQueryLib.entities;

public class AppUserWithFollowingsData : AggregateRoot<AppUserId>
{
    private AppUserWithFollowingsData() { }
    private ImmutableHashSet<AppUserId> Followers { get; }
    private ImmutableHashSet<AppUserId> Followings { get; }
    public bool IsFollowedBy(AppUserId followerId) => Followers.Contains(followerId);
    public int FollowersCount => Followers.Count;
    public int FollowingsCount => Followings.Count;
}