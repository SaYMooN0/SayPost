using MediatR;
using SayPostMainService.Domain.app_user_aggregate;
using SayPostMainService.Domain.common.interfaces.repositories;
using SayPostMainService.Domain.draft_post_aggregate.events;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Application.draft_posts.events;

public class NewDraftPostCreatedEventHandler : INotificationHandler<NewDraftPostCreatedEvent>
{
    private readonly IAppUsersRepository _appUsersRepository;

    public NewDraftPostCreatedEventHandler(IAppUsersRepository appUsersRepository) {
        _appUsersRepository = appUsersRepository;
    }

    public async Task Handle(NewDraftPostCreatedEvent notification, CancellationToken cancellationToken) {
        AppUser? user = await _appUsersRepository.GetById(notification.AuthorId);
        if (user is null) {
            throw new ErrCausedException(ErrFactory.NotFound(
                "Unable to create new post because user was not found",
                $"User id: {notification.AuthorId}"
            ));
        }
        user.AddDraftPost(notification.DraftPostId);
        await _appUsersRepository.Update(user);
    }
}