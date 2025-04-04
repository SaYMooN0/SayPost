using SayPostAuthService.Domain.common;
using SayPostAuthService.Domain.common.interfaces.repositories;
using SayPostAuthService.Domain.unconfirmed_app_user_aggregate;

namespace SayPostAuthService.Infrastructure.persistence.repositories;

internal class UnconfirmedAppUsersRepository : IUnconfirmedAppUsersRepository
{
    private AuthDbContext _db;

    public UnconfirmedAppUsersRepository(AuthDbContext db) {
        _db = db;
    }


    public Task<UnconfirmedAppUser?> GetByEmail(Email email) => 

    public Task<UnconfirmedAppUser?> GetById(UnconfirmedAppUserId userId) => 

    public Task AddNew(UnconfirmedAppUser unconfirmedAppUser) => ;

    public Task OverrideExistingWithEmail(UnconfirmedAppUser unconfirmedAppUser) =>;

    public Task RemoveById(UnconfirmedAppUserId userId) => ;
}