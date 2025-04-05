using SayPostAuthService.Domain.unconfirmed_app_user_aggregate;

namespace SayPostAuthService.Domain.common.interfaces.repositories;

public interface IUnconfirmedAppUsersRepository
{
    Task<UnconfirmedAppUser?> GetByEmail(Email email);
    Task<UnconfirmedAppUser?> GetById(UnconfirmedAppUserId userId);

    Task AddNew(UnconfirmedAppUser unconfirmedAppUser);

    Task Update(UnconfirmedAppUser unconfirmedUser);
}