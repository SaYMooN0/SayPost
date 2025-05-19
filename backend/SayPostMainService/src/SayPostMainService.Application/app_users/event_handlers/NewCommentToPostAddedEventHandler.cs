using MediatR;
using SayPostMainService.Domain.app_user_aggregate;
using SayPostMainService.Domain.common.interfaces.repositories;
using SayPostMainService.Domain.published_post_aggregate.events;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Application.app_users.event_handlers;

public class NewCommentToPostAddedEventHandler : INotificationHandler<NewCommentToPostAddedEvent>
{
    private readonly IAppUsersRepository _appUsersRepository;

    public NewCommentToPostAddedEventHandler(IAppUsersRepository appUsersRepository) {
        _appUsersRepository = appUsersRepository;
    }

    public async Task Handle(NewCommentToPostAddedEvent notification, CancellationToken cancellationToken) {
        AppUser? user = await _appUsersRepository.GetById(notification.CommentAuthorId);
        if (user is null) {
            throw new ErrCausedException(ErrFactory.NotFound(
                "Unable to add comment because user was not found",
                $"User id: {notification.CommentAuthorId}"
            ));
        }

        user.AddComment(notification.CommentId);
        await _appUsersRepository.Update(user);
    }
}