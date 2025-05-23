using FollowingsQueryLib.entities;
using SharedKernel.common.domain.ids;

namespace FollowingsQueryLib.repository;

public interface IUserFollowingsReadRepository
{
    public Task<AppUserWithFollowingsData?> GetUser(AppUserId userId);
}