using MediatR;
using SayPostMainService.Domain.app_user_aggregate;
using SayPostMainService.Domain.common.interfaces.repositories;
using SayPostMainService.Domain.draft_post_aggregate.events;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Application.draft_posts.events;

public class DraftPostDeletedEventHandler : INotificationHandler<DraftPostDeletedEvent>
{
    private readonly IAppUsersRepository _appUsersRepository;

    public DraftPostDeletedEventHandler(IAppUsersRepository appUsersRepository) {
        _appUsersRepository = appUsersRepository;
    }

    public async Task Handle(DraftPostDeletedEvent notification, CancellationToken cancellationToken) {
        AppUser? user = await _appUsersRepository.GetById(notification.PostAuthorId);
        if (user is null) {
            throw new ErrCausedException(ErrFactory.NotFound(
                "Unable to create new post because user was not found",
                $"User id: {notification.PostAuthorId}"
            ));
        }
        user.RemoveDraftPost(notification.DraftPostId);
        await _appUsersRepository.Update(user);
    }
}