using FollowingsQueryLib.repository;
using MediatR;
using SayPostNotificationService.Domain.app_user_aggregate;
using SayPostNotificationService.Domain.common.interfaces.repositories;
using SharedKernel.common.errs.utils;
using SharedKernel.date_time_provider;
using SharedKernel.integration_events;

namespace SayPostNotificationService.Application.app_users.integration_event_handlers;

internal class NewPostPublishedIntegrationEventHandler : INotificationHandler<NewPostPublishedIntegrationEvent>
{
    private readonly IAppUsersRepository _appUsersRepository;
    private readonly IUserFollowingsReadRepository _userFollowingsReadRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public NewPostPublishedIntegrationEventHandler(
        IAppUsersRepository appUsersRepository,
        IDateTimeProvider dateTimeProvider,
        IUserFollowingsReadRepository userFollowingsReadRepository
    ) {
        _appUsersRepository = appUsersRepository;
        _dateTimeProvider = dateTimeProvider;
        _userFollowingsReadRepository = userFollowingsReadRepository;
    }


    public async Task Handle(NewPostPublishedIntegrationEvent notification, CancellationToken cancellationToken) {
        var postAuthor = await _userFollowingsReadRepository.GetUser(notification.AuthorId);
        if (postAuthor is null) {
            throw new ErrCausedException(ErrFactory.NotFound(
                $"Post author with id {notification.AuthorId} was not found")
            );
        }

        var postAuthorFollowerIds = postAuthor.FollowerIds.ToArray();
        if (postAuthorFollowerIds.Length > 0) {
            var followers = await _appUsersRepository.GetUsersWithNotifications(postAuthorFollowerIds);
            var n = Notification.CreateNewTestPublished(_dateTimeProvider, notification.PostId, notification.AuthorId);

            foreach (var appUser in followers) {
                appUser.AddNotification(n);
                await _appUsersRepository.Update(appUser);
            }
        }
    }
}