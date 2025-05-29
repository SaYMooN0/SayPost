using MediatR;
using SayPostFollowingsService.Domain.app_user_aggregate;
using SayPostFollowingsService.Domain.app_user_aggregate.events;
using SayPostFollowingsService.Domain.common.interfaces.repositories;
using SharedKernel.common.errs.utils;

namespace SayPostFollowingsService.Application.app_users.event_handlers;

public class UserGotNewFollowingEventHandler : INotificationHandler<UserGotNewFollowingEvent>
{
    private readonly IAppUsersRepository _appUsersRepository;

    public UserGotNewFollowingEventHandler(IAppUsersRepository appUsersRepository) {
        _appUsersRepository = appUsersRepository;
    }

    public async Task Handle(UserGotNewFollowingEvent notification, CancellationToken cancellationToken) {
        AppUser? user = await _appUsersRepository.GetById(notification.UserThatFollowedId);
        if (user is null) {
            throw new ErrCausedException(ErrFactory.NotFound(
                "Unable to follow because the follower was not found",
                $"User id: {notification.UserThatFollowedId}"
            ));
        }

        var changesMade = user.AddFollowing(notification.FollowingId);
        if (changesMade) {
            await _appUsersRepository.Update(user);
        }
    }
}