using SayPostMainService.Domain.app_user_aggregate;

namespace SayPostMainService.Domain.common.interfaces.repositories;

public interface IAppUsersRepository
{
    Task Add(AppUser appUser);
    Task Update(AppUser appUser);
}