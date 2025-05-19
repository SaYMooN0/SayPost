using MediatR;
using SayPostMainService.Domain.app_user_aggregate;
using SayPostMainService.Domain.common.interfaces.repositories;
using SayPostMainService.Domain.published_post_aggregate.events;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Application.app_users.event_handlers;

public class PostLikedEventHandler : INotificationHandler<PostLikedEvent>
{
    private readonly IAppUsersRepository _appUsersRepository;
    public PostLikedEventHandler(IAppUsersRepository appUsersRepository) {
        _appUsersRepository = appUsersRepository;
    }

    public async Task Handle(PostLikedEvent notification, CancellationToken cancellationToken) {
        AppUser? user = await _appUsersRepository.GetById(notification.UserId);
        if (user is null) {
            throw new ErrCausedException(ErrFactory.NotFound(
                "Unable to like the post because user was not found",
                $"User id: {notification.UserId}"
            ));
        }
        user.AddLikedPost(notification.PostId);
        await _appUsersRepository.Update(user);
    }
}