using MediatR;
using SayPostFollowingsService.Domain.app_user_aggregate;
using SayPostFollowingsService.Domain.app_user_aggregate.events;
using SayPostFollowingsService.Domain.common.interfaces.repositories;
using SharedKernel.common.errs.utils;

namespace SayPostFollowingsService.Application.app_users.event_handlers;

public class UserLostFollowingEventHandler : INotificationHandler<UserLostFollowingEvent>
{
    private readonly IAppUsersRepository _appUsersRepository;

    public UserLostFollowingEventHandler(IAppUsersRepository appUsersRepository) {
        _appUsersRepository = appUsersRepository;
    }

    public async Task Handle(UserLostFollowingEvent notification, CancellationToken cancellationToken) {
        AppUser? user = await _appUsersRepository.GetById(notification.UserId);
        if (user is null) {
            throw new ErrCausedException(ErrFactory.NotFound(
                "Unable to unfollow because the follower was not found",
                $"User id: {notification.UserId}"
            ));
        }

        var changesMade = user.RemoveFollower(notification.FollowingId);
        if (changesMade) {
            await _appUsersRepository.Update(user);
        }
    }
}