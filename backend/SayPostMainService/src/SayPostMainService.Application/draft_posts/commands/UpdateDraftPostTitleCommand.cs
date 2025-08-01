using MediatR;
using SayPostMainService.Application.mediatr_behavior.restrictors;
using SayPostMainService.Domain.common;
using SayPostMainService.Domain.common.interfaces.repositories;
using SayPostMainService.Domain.common.post_aggregates_shared;
using SayPostMainService.Domain.draft_post_aggregate;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;
using SharedKernel.date_time_provider;

namespace SayPostMainService.Application.draft_posts.commands;

public record class UpdateDraftPostTitleCommand(DraftPostId DraftPostId, PostTitle NewTitle) :
    IRequest<ErrOr<(PostTitle NewTitle, DateTime NewLastModified)>>,
    IRequiresDraftPostAccessCheck;

internal class UpdateDraftPostTitleCommandHandler
    : IRequestHandler<UpdateDraftPostTitleCommand, ErrOr<(PostTitle NewTitle, DateTime NewLastModified)>>
{
    private readonly IDraftPostsRepository _draftPostsRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public UpdateDraftPostTitleCommandHandler(
        IDraftPostsRepository draftPostsRepository, IDateTimeProvider dateTimeProvider
    ) {
        _draftPostsRepository = draftPostsRepository;
        _dateTimeProvider = dateTimeProvider;
    }


    public async Task<ErrOr<(PostTitle NewTitle, DateTime NewLastModified)>> Handle(
        UpdateDraftPostTitleCommand command, CancellationToken cancellationToken
    ) {
        DraftPost? post = await _draftPostsRepository.GetById(command.DraftPostId);
        if (post is null) {
            return ErrFactory.NotFound(
                "Post not found",
                $"Post with id: {command.DraftPostId} that you are trying to update was not found"
            );
        }

        post.UpdateTitle(command.NewTitle, _dateTimeProvider);
        await _draftPostsRepository.Update(post);

        return (post.Title, post.LastModifiedAt);
    }
}