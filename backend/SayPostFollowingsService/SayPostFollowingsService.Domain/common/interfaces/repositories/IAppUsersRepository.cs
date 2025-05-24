using SayPostFollowingsService.Domain.app_user_aggregate;
using SharedKernel.common.domain.ids;

namespace SayPostFollowingsService.Domain.common.interfaces.repositories;

public interface IAppUsersRepository
{
    Task<AppUser?> GetById(AppUserId userId);
    Task Update(AppUser user);
    Task Add(AppUser appUser);

}