using System.Collections.Immutable;
using SayPostMainService.Domain.draft_post_aggregate;
using SharedKernel.common.domain.ids;

namespace SayPostMainService.Domain.common.interfaces.repositories;

public interface IDraftPostsRepository
{
    Task Add(DraftPost draftPost);
    Task Update(DraftPost draftPost);

    Task<IReadOnlyCollection<DraftPost>> GetPostsByUserWithSortingAsNoTracking(
        AppUserId authorId, DraftPostsSortOption sortOption
    );
}