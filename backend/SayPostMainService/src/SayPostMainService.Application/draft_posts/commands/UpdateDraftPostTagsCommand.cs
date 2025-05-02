using MediatR;
using SayPostMainService.Domain.common;
using SayPostMainService.Domain.common.interfaces.repositories;
using SayPostMainService.Domain.draft_post_aggregate;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;
using SharedKernel.date_time_provider;

namespace SayPostMainService.Application.draft_posts.commands;

public record class UpdateDraftPostTagsCommand(DraftPostId DraftPostId, HashSet<PostTagId> Tags) :
    IRequest<ErrOr<DraftPost>>;

internal class UpdateDraftPostTagsCommandHandler : IRequestHandler<UpdateDraftPostTagsCommand, ErrOr<DraftPost>>
{
    private readonly IDraftPostsRepository _draftPostsRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public UpdateDraftPostTagsCommandHandler(
        IDraftPostsRepository draftPostsRepository, IDateTimeProvider dateTimeProvider
    ) {
        _draftPostsRepository = draftPostsRepository;
        _dateTimeProvider = dateTimeProvider;
    }


    public async Task<ErrOr<DraftPost>> Handle(
        UpdateDraftPostTagsCommand command, CancellationToken cancellationToken
    ) {
        DraftPost? post = await _draftPostsRepository.GetById(command.DraftPostId);
        if (post is null) {
            return ErrFactory.NotFound(
                "Post not found",
                $"Post with id: {command.DraftPostId} that you are trying to update was not found"
            );
        }

        var updateRes = post.UpdateTags(command.Tags, _dateTimeProvider);
        if (updateRes.IsErr(out var err)) {
            return err;
        }

        await _draftPostsRepository.Update(post);

        return post;
    }
}