using System.Collections.Immutable;
using SharedKernel.common.domain;
using SharedKernel.common.domain.ids;

namespace FollowingsQueryLib.entities;

internal class FollowingsLibAppUser : AggregateRoot<AppUserId>
{
    private FollowingsLibAppUser() { }

    public ImmutableHashSet<AppUserId> Followers { get; }
    public ImmutableHashSet<AppUserId> Followings { get; }
}