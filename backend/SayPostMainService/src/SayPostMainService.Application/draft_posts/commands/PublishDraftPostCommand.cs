using MediatR;
using SayPostMainService.Domain.common;
using SayPostMainService.Domain.common.interfaces.repositories;
using SayPostMainService.Domain.draft_post_aggregate;
using SayPostMainService.Domain.published_post_aggregate;
using SharedKernel.common.domain.ids;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;
using SharedKernel.date_time_provider;

namespace SayPostMainService.Application.draft_posts.commands;

public record class PublishDraftPostCommand(DraftPostId DraftPostId) : IRequest<ErrOr<PublishedPostId>>;

internal class PublishDraftPostCommandHandler : IRequestHandler<PublishDraftPostCommand, ErrOr<PublishedPostId>>
{
    private readonly IPublishedPostsRepository _publishedPostsRepository;
    private readonly IDraftPostsRepository _draftPostsRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public PublishDraftPostCommandHandler(
        IDraftPostsRepository draftPostsRepository,
        IDateTimeProvider dateTimeProvider,
        IPublishedPostsRepository publishedPostsRepository
    ) {
        _draftPostsRepository = draftPostsRepository;
        _dateTimeProvider = dateTimeProvider;
        _publishedPostsRepository = publishedPostsRepository;
    }

    public async Task<ErrOr<PublishedPostId>> Handle(
        PublishDraftPostCommand request, CancellationToken cancellationToken
    ) {
        DraftPost? draftPost = await _draftPostsRepository.GetById(request.DraftPostId);
        if (draftPost is null) {
            return ErrFactory.NotFound(
                "Unknown post",
                $"Post with id: {request.DraftPostId} that you are trying to publish was not found"
            );
        }

        PublishedPost publishedPost = PublishedPost.CreateFromDraft(draftPost, _dateTimeProvider);
        await _publishedPostsRepository.Add(publishedPost);

        await _draftPostsRepository.Delete(draftPost);


        return publishedPost.Id;
    }
}