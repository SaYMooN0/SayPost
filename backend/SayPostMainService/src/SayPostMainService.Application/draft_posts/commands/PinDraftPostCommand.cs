using MediatR;
using SayPostMainService.Domain.common;
using SayPostMainService.Domain.common.interfaces.repositories;
using SayPostMainService.Domain.draft_post_aggregate;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Application.draft_posts.commands;

public record class PinDraftPostCommand(DraftPostId DraftPostId) : IRequest<ErrOr<bool>>;

internal class PinDraftPostCommandHandler : IRequestHandler<PinDraftPostCommand, ErrOr<bool>>
{
    private readonly IDraftPostsRepository _draftPostsRepository;

    public PinDraftPostCommandHandler(IDraftPostsRepository draftPostsRepository) {
        _draftPostsRepository = draftPostsRepository;
    }

    public async Task<ErrOr<bool>> Handle(
        PinDraftPostCommand request, CancellationToken cancellationToken
    ) {
        DraftPost? post = await _draftPostsRepository.GetById(request.DraftPostId);
        if (post is null) {
            return ErrFactory.NotFound(
                "Post not found",
                $"Post with id: {request.DraftPostId} that you are trying to update was not found"
            );
        }

        post.Pin();
        await _draftPostsRepository.Update(post);

        return post.IsPinned;
    }
}