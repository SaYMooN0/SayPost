using System.Collections.Immutable;
using MediatR;
using SayPostMainService.Domain.common;
using SayPostMainService.Domain.common.interfaces.repositories;
using SayPostMainService.Domain.draft_post_aggregate;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Application.draft_posts.queries;

public record class GetDraftPostByIdQuery(DraftPostId PostId) : IRequest<ErrOr<DraftPost>>;

internal class GetDraftPostByIdQueryHandler : IRequestHandler<GetDraftPostByIdQuery, ErrOr<DraftPost>>
{
    private readonly IDraftPostsRepository _draftPostsRepository;

    public GetDraftPostByIdQueryHandler(IDraftPostsRepository draftPostsRepository) {
        _draftPostsRepository = draftPostsRepository;
    }


    public async Task<ErrOr<DraftPost>> Handle(GetDraftPostByIdQuery query, CancellationToken cancellationToken) {
        var post = await _draftPostsRepository.GetById(query.PostId);
        if (post is null) {
            return ErrFactory.NotFound($"Post with id {query.PostId} was not found");
        }

        return post;
    }
}