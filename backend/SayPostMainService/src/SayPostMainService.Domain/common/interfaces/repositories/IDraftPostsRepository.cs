using SayPostMainService.Domain.draft_post_aggregate;
using SharedKernel.common.domain.ids;
using SharedKernel.common.errs;

namespace SayPostMainService.Domain.common.interfaces.repositories;

public interface IDraftPostsRepository
{
    Task Add(DraftPost draftPost);
    Task Update(DraftPost draftPost);

    Task<IReadOnlyCollection<DraftPost>> GetPostsByUserWithSortingAsNoTracking(
        AppUserId authorId, DraftPostsSortOption sortOption
    );

    Task<DraftPost?> GetById(DraftPostId draftPostId);
    Task<ErrOr<AppUserId>> GetPostAuthor(DraftPostId draftPostId);
}