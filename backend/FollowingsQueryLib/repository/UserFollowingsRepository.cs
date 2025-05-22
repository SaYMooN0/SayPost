using System.Collections.Immutable;
using FollowingsQueryLib.entities;
using SharedKernel.common.domain.ids;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace FollowingsQueryLib.repository;

internal class UserFollowingsRepository : IUserFollowingsRepository
{
    private readonly UserRelationsDbContext _db;

    public UserFollowingsRepository(UserRelationsDbContext db) => _db = db;


    public async Task<ErrOr<ImmutableHashSet<AppUserId>>> GetUserFollowings(AppUserId userId) {
        FollowingsLibAppUser? user = await _db.Users.FindAsync(userId);
        if (user is null) {
            return ErrFactory.NotFound("User not found", $"User with id: {userId} not found");
        }

        return user.Followings;
    }

    public async Task<ErrOr<ImmutableHashSet<AppUserId>>> GetUserFollowers(AppUserId userId) {
        FollowingsLibAppUser? user = await _db.Users.FindAsync(userId);
        if (user is null) {
            return ErrFactory.NotFound("User not found", $"User with id: {userId} not found");
        }

        return user.Followers;
    }


    public async Task<ErrOr<bool>> IsFollowing(AppUserId followerId, AppUserId userToFollowId) {
        FollowingsLibAppUser? userToFollow = await _db.Users.FindAsync(userToFollowId);
        if (userToFollow is null) {
            return ErrFactory.NotFound("Unknown user to follow", $"User to follow with id: {userToFollowId} not found");
        }

        return userToFollow.Followers.Contains(followerId);
    }
}