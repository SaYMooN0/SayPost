using MediatR;
using SayPostMainService.Domain.common.interfaces.repositories;
using SayPostMainService.Domain.post_comment_aggregate.events;
using SayPostMainService.Domain.published_post_aggregate;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Application.published_posts.event_handlers;

public class NewPostCommentCreatedEventHandler : INotificationHandler<NewPostCommentCreatedEvent>
{
    private readonly IPublishedPostsRepository _publishedPostsRepository;
    public NewPostCommentCreatedEventHandler(IPublishedPostsRepository publishedPostsRepository) {
        _publishedPostsRepository = publishedPostsRepository;
    }

    public async Task Handle(NewPostCommentCreatedEvent notification, CancellationToken cancellationToken) {
        PublishedPost? post = await _publishedPostsRepository.GetById(notification.PostId);
        if (post is null) {
            throw new ErrCausedException(ErrFactory.NotFound(
                "Unable to add comment because post was not found",
                $"Post id: {notification.PostId}"
            ));
        }

        post.AddComment(notification.CommentId, notification.CommentAuthorId);
        await _publishedPostsRepository.Update(post);
    }
}