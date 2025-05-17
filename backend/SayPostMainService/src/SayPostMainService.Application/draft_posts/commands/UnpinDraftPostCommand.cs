using MediatR;
using SayPostMainService.Application.mediatr_behavior.restrictors;
using SayPostMainService.Domain.common;
using SayPostMainService.Domain.common.interfaces.repositories;
using SayPostMainService.Domain.draft_post_aggregate;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Application.draft_posts.commands;

public record class UnpinDraftPostCommand(DraftPostId DraftPostId) :
    IRequest<ErrOr<bool>>,
    IRequiresDraftPostAccessCheck;

internal class UnpinDraftPostCommandHandler : IRequestHandler<UnpinDraftPostCommand, ErrOr<bool>>
{
    private readonly IDraftPostsRepository _draftPostsRepository;

    public UnpinDraftPostCommandHandler(IDraftPostsRepository draftPostsRepository) {
        _draftPostsRepository = draftPostsRepository;
    }

    public async Task<ErrOr<bool>> Handle(
        UnpinDraftPostCommand command, CancellationToken cancellationToken
    ) {
        DraftPost? post = await _draftPostsRepository.GetById(command.DraftPostId);
        if (post is null) {
            return ErrFactory.NotFound(
                "Post not found",
                $"Post with id: {command.DraftPostId} that you are trying to update was not found"
            );
        }

        post.Unpin();
        await _draftPostsRepository.Update(post);

        return post.IsPinned;
    }
}