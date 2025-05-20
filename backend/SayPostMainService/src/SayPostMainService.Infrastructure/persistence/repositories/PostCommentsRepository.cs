using Microsoft.EntityFrameworkCore;
using SayPostMainService.Domain.common.interfaces.repositories;
using SayPostMainService.Domain.post_comment_aggregate;
using SharedKernel.common.domain.ids;

namespace SayPostMainService.Infrastructure.persistence.repositories;

internal class PostCommentsRepository : IPostCommentsRepository
{
    private MainDbContext _db;

    public PostCommentsRepository(MainDbContext db) {
        _db = db;
    }

    public async Task<IReadOnlyCollection<PostComment>> GetCommentsForPostAsNoTracking(
        PublishedPostId postId, CommentsSortOption sortOption
    ) =>
        (
            await _db.PostComments
                .AsNoTracking()
                .Where(c => c.PostId == postId)
                .OrderBySortOption(sortOption)
                .ToListAsync()
        )
        .AsReadOnly();
}

file static class PostCommentsRepositoryExtension
{
    public static IOrderedQueryable<PostComment> OrderBySortOption(
        this IQueryable<PostComment> query,
        CommentsSortOption sortOption
    ) => sortOption switch {
        CommentsSortOption.Newest => query.OrderByDescending(p => p.CreatedAt),
        CommentsSortOption.Oldest => query.OrderBy(p => p.CreatedAt),
        _ => throw new ArgumentOutOfRangeException(
            $"Unexpected {nameof(sortOption)} value: {sortOption} in {nameof(OrderBySortOption)}"
        )
    };
}