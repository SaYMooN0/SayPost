using MediatR;
using SayPostMainService.Domain.common;
using SayPostMainService.Domain.common.interfaces.repositories;
using SayPostMainService.Domain.common.post_aggregates_shared;
using SayPostMainService.Domain.common.post_aggregates_shared.post_content;
using SayPostMainService.Domain.draft_post_aggregate;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;
using SharedKernel.date_time_provider;

namespace SayPostMainService.Application.draft_posts.commands;

public record class UpdateDraftPostContentCommand(DraftPostId DraftPostId, PostContent NewContent) :
    IRequest<ErrOr<(PostContent NewContent, DateTime NewLastModified)>>;

internal class UpdateDraftPostContentCommandHandler
    : IRequestHandler<UpdateDraftPostContentCommand, ErrOr<(PostContent NewContent, DateTime NewLastModified)>>
{
    private readonly IDraftPostsRepository _draftPostsRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public UpdateDraftPostContentCommandHandler(
        IDraftPostsRepository draftPostsRepository, IDateTimeProvider dateTimeProvider
    ) {
        _draftPostsRepository = draftPostsRepository;
        _dateTimeProvider = dateTimeProvider;
    }


    public async Task<ErrOr<(PostContent NewContent, DateTime NewLastModified)>> Handle(
        UpdateDraftPostContentCommand command, CancellationToken cancellationToken
    ) {
        DraftPost? post = await _draftPostsRepository.GetById(command.DraftPostId);
        if (post is null) {
            return ErrFactory.NotFound(
                "Post not found",
                $"Post with id: {command.DraftPostId} that you are trying to update was not found"
            );
        }

        post.UpdateContent(command.NewContent, _dateTimeProvider);
        await _draftPostsRepository.Update(post);

        return (post.Content, post.LastModifiedAt);
    }
}