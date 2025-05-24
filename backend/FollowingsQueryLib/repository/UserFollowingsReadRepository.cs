using System.Collections.Immutable;
using FollowingsQueryLib.entities;
using Microsoft.EntityFrameworkCore;
using SharedKernel.common.domain.ids;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace FollowingsQueryLib.repository;

internal class UserFollowingsReadRepository : IUserFollowingsReadRepository
{
    private readonly UserRelationsDbContext _db;

    public UserFollowingsReadRepository(UserRelationsDbContext db) => _db = db;


    public Task<AppUserWithFollowingsData?> GetUser(AppUserId userId) =>
        _db.AppUsers.AsNoTracking(). FirstOrDefaultAsync(u => u.Id == userId);
}