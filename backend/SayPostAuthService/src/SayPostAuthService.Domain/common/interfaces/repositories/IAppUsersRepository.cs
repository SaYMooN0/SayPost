using SayPostAuthService.Domain.app_user_aggregate;

namespace SayPostAuthService.Domain.common.interfaces.repositories;

public interface IAppUsersRepository
{
    Task Add(AppUser appUser);
    Task<bool> AnyUserWithEmail(Email email);
    Task<AppUser?> GetByEmailAsNoTracking(Email email);
}
