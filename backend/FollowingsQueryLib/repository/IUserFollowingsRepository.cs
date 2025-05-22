using System.Collections.Immutable;
using SharedKernel.common.domain.ids;
using SharedKernel.common.errs;

namespace FollowingsQueryLib.repository;

public interface IUserFollowingsRepository
{
    public Task<ErrOr<ImmutableHashSet<AppUserId>>> GetUserFollowings(AppUserId userId);
    public Task<ErrOr<ImmutableHashSet<AppUserId>>> GetUserFollowers(AppUserId userId);
    public Task<ErrOr<bool>> IsFollowing(AppUserId followerId, AppUserId userToFollowId);
}