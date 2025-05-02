using MediatR;
using SayPostMainService.Domain.common.interfaces.repositories;
using SayPostMainService.Domain.published_post_aggregate;
using SharedKernel.common.domain.ids;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;
using SharedKernel.date_time_provider;

namespace SayPostMainService.Application.published_posts.commands;

public record class CommentPostCommand(PublishedPostId PostId, string Content, AppUserId CommentAuthor) :
    IRequest<ErrOr<PostComment>>;

internal class CommentPostCommandHandler : IRequestHandler<CommentPostCommand, ErrOr<PostComment>>
{
    private readonly IPublishedPostsRepository _publishedPostsRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CommentPostCommandHandler(
        IPublishedPostsRepository publishedPostsRepository, IDateTimeProvider dateTimeProvider
    ) {
        _publishedPostsRepository = publishedPostsRepository;
        _dateTimeProvider = dateTimeProvider;
    }


    public async Task<ErrOr<PostComment>> Handle(
        CommentPostCommand command, CancellationToken cancellationToken
    ) {
        PublishedPost? post = await _publishedPostsRepository.GetWithCommentsById(command.PostId);
        if (post is null) {
            return ErrFactory.NotFound(
                "Post not found",
                $"Post with id: {command.PostId} that you are trying to comments was not found"
            );
        }

        var comment = PostComment.CreateNew(command.Content, command.CommentAuthor, _dateTimeProvider);
        if (comment.IsErr(out var err)) {
            return err;
        }

        post.AddComment(comment.AsSuccess());
        await _publishedPostsRepository.Update(post);

        return comment;
    }
}