using System.Collections.Immutable;
using MediatR;
using SayPostMainService.Domain.common.interfaces.repositories;
using SayPostMainService.Domain.draft_post_aggregate;
using SharedKernel.common.domain.ids;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Application.draft_posts.queries;

public record class ListDraftPostsForUserQuery(AppUserId UserId, string SortOption, bool PutPinnedOnTop) :
    IRequest<ErrOr<ImmutableArray<DraftPost>>>;

internal class ListDraftPostsForUserQueryHandler
    : IRequestHandler<ListDraftPostsForUserQuery, ErrOr<ImmutableArray<DraftPost>>>
{
    private readonly IDraftPostsRepository _draftPostsRepository;

    public ListDraftPostsForUserQueryHandler(IDraftPostsRepository draftPostsRepository) {
        _draftPostsRepository = draftPostsRepository;
    }


    public async Task<ErrOr<ImmutableArray<DraftPost>>> Handle(
        ListDraftPostsForUserQuery request, CancellationToken cancellationToken
    ) {
        DraftPostsSortOption sortOption;
        try {
            sortOption = Enum.Parse<DraftPostsSortOption>(request.SortOption, ignoreCase: true);
        }
        catch {
            return ErrFactory.InvalidData($"Incorrect sort option provided: {request.SortOption}");
        }

        var posts = await _draftPostsRepository.GetPostsByUserWithSortingAsNoTracking(
            request.UserId, sortOption, request.PutPinnedOnTop
        );
        return posts.ToImmutableArray();
    }
}