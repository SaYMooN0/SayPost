using SayPostMainService.Domain.app_user_aggregate;
using SayPostMainService.Domain.common.interfaces.repositories;

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
}