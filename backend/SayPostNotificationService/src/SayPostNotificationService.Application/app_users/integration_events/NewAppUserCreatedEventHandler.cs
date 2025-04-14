using MediatR;
using SayPostNotificationService.Domain.app_user_aggregate;
using SayPostNotificationService.Domain.common.interfaces.repositories;
using SharedKernel.integration_events;

namespace SayPostNotificationService.Application.app_users.integration_events;

internal class NewAppUserCreatedEventHandler : INotificationHandler<NewAppUserCreatedIntegrationEvent>
{
    private readonly IAppUsersRepository _appUsersRepository;

    public NewAppUserCreatedEventHandler(IAppUsersRepository appUsersRepository) {
        _appUsersRepository = appUsersRepository;
    }

    public async Task Handle(NewAppUserCreatedIntegrationEvent notification, CancellationToken cancellationToken) {
        AppUser newUser = new(notification.CreatedUserId);
        await _appUsersRepository.Add(newUser);
    }
}