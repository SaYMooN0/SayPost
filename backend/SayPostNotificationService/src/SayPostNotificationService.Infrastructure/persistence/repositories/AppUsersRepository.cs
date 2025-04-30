using Microsoft.EntityFrameworkCore;
using SayPostNotificationService.Domain.app_user_aggregate;
using SayPostNotificationService.Domain.common.interfaces.repositories;
using SharedKernel.common.domain.ids;

namespace SayPostNotificationService.Infrastructure.persistence.repositories;

internal class AppUsersRepository : IAppUsersRepository
{
    private NotificationDbContext _db;

    public AppUsersRepository(NotificationDbContext db) {
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

    public Task<AppUser[]> GetAllWithNotifications() => _db.AppUsers
        .Include(u => EF.Property<ICollection<Notification>>(u, "_notifications"))
        .ToArrayAsync();

    public Task<AppUser?> GetByIdWithNotifications(AppUserId appUserId) => _db.AppUsers
        .Include(u => EF.Property<ICollection<Notification>>(u, "_notifications"))
        .FirstOrDefaultAsync(u => u.Id == appUserId);
}