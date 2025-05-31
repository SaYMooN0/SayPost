using SayPostAuthService.Domain.app_user_aggregate;
using SharedKernel.common.domain.ids;

namespace SayPostAuthService.Domain.common.interfaces.repositories;

public interface IAppUsersRepository
{
    Task Add(AppUser appUser);
    Task<bool> AnyUserWithEmail(Email email);
    Task<AppUser?> GetByEmailAsNoTracking(Email email);
    Task<AppUser?> GetByIdAsNoTracking(AppUserId id);
    Task<Dictionary<AppUserId, string>> GetUsernamesForUsers(AppUserId[] userIds);
}