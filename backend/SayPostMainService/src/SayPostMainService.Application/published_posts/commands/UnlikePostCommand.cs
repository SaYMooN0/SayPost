using MediatR;
using SayPostMainService.Application.application_layer_interfaces;
using SayPostMainService.Domain.common.interfaces.repositories;
using SayPostMainService.Domain.published_post_aggregate;
using SharedKernel.common.domain.ids;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Application.published_posts.commands;

public record class UnlikePostCommand(PublishedPostId PostId) : IRequest<ErrOrNothing>;

internal class UnlikePostCommandHandler : IRequestHandler<UnlikePostCommand, ErrOrNothing>
{
    private readonly IPublishedPostsRepository _publishedPostsRepository;
    private readonly ICurrentActorProvider _currentActorProvider;

    public UnlikePostCommandHandler(
        IPublishedPostsRepository publishedPostsRepository,
        ICurrentActorProvider currentActorProvider
    ) {
        _publishedPostsRepository = publishedPostsRepository;
        _currentActorProvider = currentActorProvider;
    }


    public async Task<ErrOrNothing> Handle(
        UnlikePostCommand command, CancellationToken cancellationToken
    ) {
        PublishedPost? post = await _publishedPostsRepository.GetById(command.PostId);
        if (post is null) {
            return ErrFactory.NotFound(
                "Post not found",
                $"Post with id: {command.PostId} that you are trying to unlike was not found"
            );
        }

        var res = post.Unlike(_currentActorProvider.AppUserId);
        if (res.IsErr(out var err)) {
            return err;
        }

        await _publishedPostsRepository.Update(post);
        return ErrOrNothing.Nothing;
    }
}