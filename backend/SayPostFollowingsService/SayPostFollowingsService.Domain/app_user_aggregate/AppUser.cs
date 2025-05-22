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
}