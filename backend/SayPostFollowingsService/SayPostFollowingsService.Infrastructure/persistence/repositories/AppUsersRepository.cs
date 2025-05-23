using SayPostFollowingsService.Domain.app_user_aggregate;
using SayPostFollowingsService.Domain.common.interfaces.repositories;
using SharedKernel.common.domain.ids;

namespace SayPostFollowingsService.Infrastructure.persistence.repositories;

internal class AppUsersRepository : IAppUsersRepository
{
    private FollowingDbContext _db;

    public AppUsersRepository(FollowingDbContext db) {
        _db = db;
    }

    public async Task<AppUser?> GetById(AppUserId userId) =>
        await _db.AppUsers.FindAsync(userId);

    public async Task Update(AppUser user) {
        _db.AppUsers.Update(user);
        await _db.SaveChangesAsync();
    }
}