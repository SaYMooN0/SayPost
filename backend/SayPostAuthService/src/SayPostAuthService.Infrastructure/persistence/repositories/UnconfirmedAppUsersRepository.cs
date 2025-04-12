using Microsoft.EntityFrameworkCore;
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


    public async Task<UnconfirmedAppUser?> GetByEmail(Email email) =>
        await _db.UnconfirmedAppUsers.FirstOrDefaultAsync(u => u.Email == email);


    public async Task<UnconfirmedAppUser?> GetById(UnconfirmedAppUserId userId) =>
        await _db.UnconfirmedAppUsers.FindAsync(userId);

    public async Task AddNew(UnconfirmedAppUser unconfirmedAppUser) {
        await _db.UnconfirmedAppUsers.AddAsync(unconfirmedAppUser);
        await _db.SaveChangesAsync();
    }

    public async Task Update(UnconfirmedAppUser unconfirmedUser) {
        _db.UnconfirmedAppUsers.Update(unconfirmedUser);
        await _db.SaveChangesAsync();
    }

    public async Task Remove(UnconfirmedAppUser user) {
        _db.UnconfirmedAppUsers.Remove(user);
        await _db.SaveChangesAsync();
    }
}