using Microsoft.EntityFrameworkCore;
using SayPostAuthService.Domain.app_user_aggregate;
using SayPostAuthService.Domain.common;
using SayPostAuthService.Domain.common.interfaces.repositories;
using SharedKernel.common.domain.ids;

namespace SayPostAuthService.Infrastructure.persistence.repositories;

internal class AppUsersRepository : IAppUsersRepository
{
    private AuthDbContext _db;

    public AppUsersRepository(AuthDbContext db) {
        _db = db;
    }


    public async Task Add(AppUser appUser) {
        await _db.AppUsers.AddAsync(appUser);
        await _db.SaveChangesAsync();
    }

    public async Task<bool> AnyUserWithEmail(Email email) =>
        await _db.AppUsers.AnyAsync(u => u.Email == email);

    public async Task<AppUser?> GetByEmailAsNoTracking(Email email) =>
        await _db.AppUsers
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email);

    public async Task<AppUser?> GetByIdAsNoTracking(AppUserId id) => await
        _db.AppUsers
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);

    public async Task<Dictionary<AppUserId, string>> GetUsernamesForUsers(AppUserId[] userIds) =>
        await _db.AppUsers
            .AsNoTracking()
            .Where(u => userIds.Contains(u.Id))
            .ToDictionaryAsync(u => u.Id, u => u.Username);
}