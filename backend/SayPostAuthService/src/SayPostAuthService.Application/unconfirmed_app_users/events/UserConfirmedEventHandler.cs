using MediatR;
using SayPostAuthService.Domain.app_user_aggregate;
using SayPostAuthService.Domain.app_user_aggregate.events;
using SayPostAuthService.Domain.common.interfaces.repositories;

namespace SayPostAuthService.Application.unconfirmed_app_users.events;

internal class UserConfirmedEventHandler : INotificationHandler<UserConfirmedEvent>
{
    private readonly IAppUsersRepository _appUsersRepository;
    private readonly IUnconfirmedAppUsersRepository _unconfirmedAppUsersRepository;

    public UserConfirmedEventHandler(
        IAppUsersRepository appUsersRepository,
        IUnconfirmedAppUsersRepository unconfirmedAppUsersRepository
    ) {
        _appUsersRepository = appUsersRepository;
        _unconfirmedAppUsersRepository = unconfirmedAppUsersRepository;
    }

    public async Task Handle(UserConfirmedEvent notification, CancellationToken cancellationToken) {
        var appUser = AppUser.CreateNew(notification.Email, notification.PasswordHash);
        await _unconfirmedAppUsersRepository.RemoveById(notification.UnconfirmedAppUserId);
        await _appUsersRepository.Add(appUser);
    }
}