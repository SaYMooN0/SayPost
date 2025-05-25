using MediatR;
using SayPostNotificationService.Domain.app_user_aggregate;
using SayPostNotificationService.Domain.common.interfaces.repositories;
using SharedKernel.common.errs.utils;
using SharedKernel.date_time_provider;
using SharedKernel.integration_events;

namespace SayPostNotificationService.Application.app_users.integration_event_handlers;

internal class PostLikedIntegrationEventHandler : INotificationHandler<PostLikedIntegrationEvent>
{
    private readonly IAppUsersRepository _appUsersRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public PostLikedIntegrationEventHandler(
        IAppUsersRepository appUsersRepository,
        IDateTimeProvider dateTimeProvider
    ) {
        _appUsersRepository = appUsersRepository;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task Handle(PostLikedIntegrationEvent notification, CancellationToken cancellationToken) {
        AppUser? postAuthor = await _appUsersRepository.GetByIdWithNotifications(notification.PostAuthorId);
        if (postAuthor is null) {
            throw new ErrCausedException(ErrFactory.NotFound(
                $"Post author with id {notification.PostAuthorId} was not found")
            );
        }

        var n = Notification.CreateNewPostLiked(_dateTimeProvider, notification.PostId, notification.UserThatLikedId);
        postAuthor.AddNotification(n);
        await _appUsersRepository.Update(postAuthor);
    }
}