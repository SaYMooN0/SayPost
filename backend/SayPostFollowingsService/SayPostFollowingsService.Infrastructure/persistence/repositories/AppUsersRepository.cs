using SayPostFollowingsService.Domain.common.interfaces.repositories;

namespace SayPostFollowingsService.Infrastructure.persistence.repositories;

internal class AppUsersRepository : IAppUsersRepository
{
    private FollowingDbContext _db;

    public AppUsersRepository(FollowingDbContext db) {
        _db = db;
    }

}