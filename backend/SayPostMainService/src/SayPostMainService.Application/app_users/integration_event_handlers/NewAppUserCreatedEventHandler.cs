﻿using MediatR;
using SayPostMainService.Domain.app_user_aggregate;
using SayPostMainService.Domain.common.interfaces.repositories;
using SharedKernel.integration_events;

namespace SayPostMainService.Application.app_users.integration_event_handlers;

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