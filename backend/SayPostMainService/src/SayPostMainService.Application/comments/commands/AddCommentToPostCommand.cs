using MediatR;
using SayPostMainService.Application.application_layer_interfaces;
using SayPostMainService.Domain.common.interfaces.repositories;
using SayPostMainService.Domain.post_comment_aggregate;
using SayPostMainService.Domain.published_post_aggregate;
using SharedKernel.common.domain.ids;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;
using SharedKernel.date_time_provider;

namespace SayPostMainService.Application.comments.commands;

public record class AddCommentToPostCommand(PublishedPostId PostId, string Content) :
    IRequest<ErrOr<PostComment>>;

internal class AddCommentToPostCommandHandler : IRequestHandler<AddCommentToPostCommand, ErrOr<PostComment>>
{
    private readonly IPublishedPostsRepository _publishedPostsRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ICurrentActorProvider _currentActorProvider;
    private readonly IPostCommentsRepository _postCommentsRepository;


    public AddCommentToPostCommandHandler(
        IPublishedPostsRepository publishedPostsRepository,
        IDateTimeProvider dateTimeProvider,
        ICurrentActorProvider currentActorProvider,
        IPostCommentsRepository postCommentsRepository
    ) {
        _publishedPostsRepository = publishedPostsRepository;
        _dateTimeProvider = dateTimeProvider;
        _currentActorProvider = currentActorProvider;
        _postCommentsRepository = postCommentsRepository;
    }


    public async Task<ErrOr<PostComment>> Handle(
        AddCommentToPostCommand command, CancellationToken cancellationToken
    ) {
        PublishedPost? post = await _publishedPostsRepository.GetById(command.PostId);
        if (post is null) {
            return ErrFactory.NotFound(
                "Post not found",
                $"Post with id: {command.PostId} that you are trying to comments was not found"
            );
        }

        var creationRes = PostComment.CreateNew(
            command.Content,
            command.PostId,
            _currentActorProvider.AppUserId,
            _dateTimeProvider
        );

        if (creationRes.IsErr(out var err)) {
            return err;
        }

        var comment = creationRes.AsSuccess();
        await _postCommentsRepository.Add(comment);
        return comment;
    }
}