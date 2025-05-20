using MediatR;
using SayPostMainService.Application.application_layer_interfaces;
using SayPostMainService.Domain.common.interfaces.repositories;
using SayPostMainService.Domain.published_post_aggregate;
using SharedKernel.common.domain.ids;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Application.published_posts.commands;

public record class LikePostCommand(PublishedPostId PostId) : IRequest<ErrOr<int>>;

internal class LikePostCommandHandler : IRequestHandler<LikePostCommand, ErrOr<int>>
{
    private readonly IPublishedPostsRepository _publishedPostsRepository;
    private readonly ICurrentActorProvider _currentActorProvider;

    public LikePostCommandHandler(
        IPublishedPostsRepository publishedPostsRepository,
        ICurrentActorProvider currentActorProvider
    ) {
        _publishedPostsRepository = publishedPostsRepository;
        _currentActorProvider = currentActorProvider;
    }


    public async Task<ErrOr<int>> Handle(
        LikePostCommand command, CancellationToken cancellationToken
    ) {
        PublishedPost? post = await _publishedPostsRepository.GetById(command.PostId);
        if (post is null) {
            return ErrFactory.NotFound(
                "Post not found",
                $"Post with id: {command.PostId} that you are trying to like was not found"
            );
        }

        var res = post.Like(_currentActorProvider.AppUserId);
        if (res.IsErr(out var err)) {
            return err;
        }

        await _publishedPostsRepository.Update(post);
        return post.LikesCount;
    }
}