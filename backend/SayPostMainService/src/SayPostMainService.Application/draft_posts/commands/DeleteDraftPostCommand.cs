using MediatR;
using SayPostMainService.Application.mediatr_behavior.restrictors;
using SayPostMainService.Domain.common;
using SayPostMainService.Domain.common.interfaces.repositories;
using SayPostMainService.Domain.draft_post_aggregate;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Application.draft_posts.commands;

public record class DeleteDraftPostCommand(DraftPostId DraftPostId) :
    IRequest<ErrOrNothing>,
    IRequiresDraftPostAccessCheck;

internal class DeleteDraftPostCommandHandler : IRequestHandler<DeleteDraftPostCommand, ErrOrNothing>
{
    private readonly IDraftPostsRepository _draftPostsRepository;

    public DeleteDraftPostCommandHandler(IDraftPostsRepository draftPostsRepository) {
        _draftPostsRepository = draftPostsRepository;
    }

    public async Task<ErrOrNothing> Handle(
        DeleteDraftPostCommand command, CancellationToken cancellationToken
    ) {
        DraftPost? post = await _draftPostsRepository.GetById(command.DraftPostId);
        if (post is null) {
            return ErrFactory.NotFound(
                "Post not found",
                $"Post with id: {command.DraftPostId} that you are trying to update was not found"
            );
        }

        post.Delete();
        await _draftPostsRepository.Delete(post);

        return ErrOrNothing.Nothing;
    }
}