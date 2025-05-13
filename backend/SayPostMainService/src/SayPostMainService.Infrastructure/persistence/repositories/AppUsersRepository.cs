using Microsoft.EntityFrameworkCore;
using SayPostMainService.Domain.app_user_aggregate;
using SayPostMainService.Domain.common.interfaces.repositories;
using SharedKernel.common.domain.ids;

namespace SayPostMainService.Infrastructure.persistence.repositories;

internal class AppUsersRepository : IAppUsersRepository
{
    private MainDbContext _db;

    public AppUsersRepository(MainDbContext db) {
        _db = db;
    }


    public async Task Add(AppUser appUser) {
        await _db.AppUsers.AddAsync(appUser);
        await _db.SaveChangesAsync();
    }

    public async Task Update(AppUser appUser) {
        _db.AppUsers.Update(appUser);
        await _db.SaveChangesAsync();
    }

    public async Task<AppUser?> GetById(AppUserId userId) =>
        await _db.AppUsers.FindAsync(userId);

    public async Task<AppUser?> GetWithBannerAsNoTracking(AppUserId userId) =>
        await _db.AppUsers
            .AsNoTracking()
            .Include(u => u.ProfileBanner)
            .FirstOrDefaultAsync(u => u.Id == userId);

    public async Task<bool> DoesUserExist(AppUserId userId) =>
        await _db.AppUsers.AnyAsync(u => u.Id == userId);
}