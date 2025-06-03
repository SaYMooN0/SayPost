using MediatR;
using SayPostMainService.Domain.app_user_aggregate;
using SayPostMainService.Domain.common.interfaces.repositories;
using SayPostMainService.Domain.published_post_aggregate.events;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Application.app_users.event_handlers;

public class NewPublishedPostCreatedEventHandler : INotificationHandler<NewPublishedPostCreatedEvent>
{
    private readonly IAppUsersRepository _appUsersRepository;

    public NewPublishedPostCreatedEventHandler(IAppUsersRepository appUsersRepository) {
        _appUsersRepository = appUsersRepository;
    }

    public async Task Handle(NewPublishedPostCreatedEvent notification, CancellationToken cancellationToken) {
        AppUser? user = await _appUsersRepository.GetById(notification.AuthorId);
        if (user is null) {
            throw new ErrCausedException(ErrFactory.NotFound(
                "Unable to publish post because user was not found",
                $"User id: {notification.AuthorId}"
            ));
        }

        user.AddPublishedPost(notification.PostId);
        await _appUsersRepository.Update(user);
    }
}