using MediatR;
using SayPostNotificationService.Domain.app_user_aggregate;
using SayPostNotificationService.Domain.common.interfaces.repositories;
using SharedKernel.common.errs.utils;
using SharedKernel.date_time_provider;
using SharedKernel.integration_events;

namespace SayPostNotificationService.Application.app_users.integration_event_handlers;

internal class UserGotNewFollowerIntegrationEventHandler : INotificationHandler<UserGotNewFollowerIntegrationEvent>
{
    private readonly IAppUsersRepository _appUsersRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public UserGotNewFollowerIntegrationEventHandler(
        IAppUsersRepository appUsersRepository, IDateTimeProvider dateTimeProvider
    ) {
        _appUsersRepository = appUsersRepository;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task Handle(UserGotNewFollowerIntegrationEvent notification, CancellationToken cancellationToken) {
        AppUser? user = await _appUsersRepository.GetByIdWithNotifications(notification.UserId);
        if (user is null) {
            throw new ErrCausedException(ErrFactory.NotFound(
                $"User with id {notification.UserId} was not found")
            );
        }

        var n = Notification.CreateNewUserGotFollower(_dateTimeProvider, notification.FollowerId);
        user.AddNotification(n);
        await _appUsersRepository.Update(user);
    }
}