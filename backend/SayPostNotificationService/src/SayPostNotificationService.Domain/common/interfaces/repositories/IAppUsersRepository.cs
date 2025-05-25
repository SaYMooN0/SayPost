using SayPostNotificationService.Domain.app_user_aggregate;
using SharedKernel.common.domain.ids;

namespace SayPostNotificationService.Domain.common.interfaces.repositories;

public interface IAppUsersRepository
{
    Task Add(AppUser appUser);
    Task Update(AppUser appUser);
    Task<AppUser[]> GetUsersWithNotifications(AppUserId[] userIds);
    Task<AppUser?> GetByIdWithNotifications(AppUserId appUserId);
}