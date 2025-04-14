using SayPostNotificationService.Domain.app_user_aggregate;

namespace SayPostNotificationService.Domain.common.interfaces.repositories;

public interface IAppUsersRepository
{
    Task Add(AppUser appUser);
    Task Update(AppUser appUser);
}