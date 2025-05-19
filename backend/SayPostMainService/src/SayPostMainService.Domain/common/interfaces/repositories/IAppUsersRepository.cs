using SayPostMainService.Domain.app_user_aggregate;
using SharedKernel.common.domain.ids;

namespace SayPostMainService.Domain.common.interfaces.repositories;

public interface IAppUsersRepository
{
    Task Add(AppUser appUser);
    Task Update(AppUser appUser);
    Task<AppUser?> GetById(AppUserId userId);
    Task<AppUser?> GetWithBannerAsNoTracking(AppUserId userId);
    Task<AppUser?> GetWithBanner(AppUserId userId);
    Task<bool> DoesUserExist(AppUserId userId);
}