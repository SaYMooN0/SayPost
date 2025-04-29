using MediatR;
using SayPostNotificationService.Domain.app_user_aggregate;
using SayPostNotificationService.Domain.common.interfaces.repositories;
using SharedKernel.date_time_provider;
using SharedKernel.integration_events;

namespace SayPostNotificationService.Application.app_users.integration_events;

internal class NewPostPublishedIntegrationEventHandler : INotificationHandler<NewPostPublishedIntegrationEvent>
{
    private readonly IAppUsersRepository _appUsersRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public NewPostPublishedIntegrationEventHandler(
        IAppUsersRepository appUsersRepository,
        IDateTimeProvider dateTimeProvider
    ) {
        _appUsersRepository = appUsersRepository;
        _dateTimeProvider = dateTimeProvider;
    }


    public async Task Handle(NewPostPublishedIntegrationEvent notification, CancellationToken cancellationToken) {
        var n = Notification.CreateNewTestPublished(_dateTimeProvider, notification.PostId, notification.AuthorId);
        //for now all users will receive notifications 
        var appUsers = await _appUsersRepository.GetAllWithNotifications();
        foreach (var appUser in appUsers) {
            appUser.AddNotification(n);
            await _appUsersRepository.Update(appUser);
        }
    }
}