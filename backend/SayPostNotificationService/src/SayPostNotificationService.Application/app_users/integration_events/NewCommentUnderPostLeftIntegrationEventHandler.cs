using MediatR;
using SayPostNotificationService.Domain.app_user_aggregate;
using SayPostNotificationService.Domain.common.interfaces.repositories;
using SharedKernel.common.errs.utils;
using SharedKernel.date_time_provider;
using SharedKernel.integration_events;

namespace SayPostNotificationService.Application.app_users.integration_events;

internal class NewCommentUnderPostLeftIntegrationEventHandler :
    INotificationHandler<NewCommentUnderPostLeftIntegrationEvent>
{
    private readonly IAppUsersRepository _appUsersRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public NewCommentUnderPostLeftIntegrationEventHandler(
        IAppUsersRepository appUsersRepository,
        IDateTimeProvider dateTimeProvider
    ) {
        _appUsersRepository = appUsersRepository;
        _dateTimeProvider = dateTimeProvider;
    }


    public async Task Handle(
        NewCommentUnderPostLeftIntegrationEvent notification, CancellationToken cancellationToken
    ) {
        AppUser? postAuthor = await _appUsersRepository.GetByIdWithNotifications(notification.PostAuthorId);
        if (postAuthor is null) {
            throw new ErrCausedException(ErrFactory.NotFound(
                $"Post author with id {notification.PostAuthorId} was not found")
            );
        }

        var n = Notification.CreateNewCommentLeft(
            _dateTimeProvider, notification.PostTitle, notification.CommentAuthorId
        );
        postAuthor.AddNotification(n);
        await _appUsersRepository.Update(postAuthor);
    }
}