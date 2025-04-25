using Microsoft.EntityFrameworkCore;
using SayPostMainService.Domain.common;
using SayPostMainService.Domain.common.interfaces.repositories;
using SayPostMainService.Domain.draft_post_aggregate;
using SharedKernel.common.domain.ids;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Infrastructure.persistence.repositories;

internal class DraftPostsRepository : IDraftPostsRepository
{
    private MainDbContext _db;

    public DraftPostsRepository(MainDbContext db) {
        _db = db;
    }


    public async Task Add(DraftPost draftPost) {
        await _db.DraftPosts.AddAsync(draftPost);
        await _db.SaveChangesAsync();
    }

    public async Task Update(DraftPost draftPost) {
        _db.DraftPosts.Update(draftPost);
        await _db.SaveChangesAsync();
    }

    public async Task<IReadOnlyCollection<DraftPost>> GetPostsByUserWithSortingAsNoTracking(
        AppUserId userId,
        DraftPostsSortOption sortOption,
        bool putPinnedOnTop
    ) => await _db.DraftPosts
        .Where(p => p.AuthorId == userId)
        .OrderByPinnedThenSortOption(putPinnedOnTop, sortOption)
        .ToArrayAsync();

    public async Task<DraftPost?> GetById(DraftPostId draftPostId) =>
        await _db.DraftPosts.FindAsync(draftPostId);

    public async Task<ErrOr<AppUserId>> GetPostAuthor(DraftPostId draftPostId) {
        DraftPost? draftPost = await GetById(draftPostId);
        if (draftPost is null) {
            return ErrFactory.NotFound(
                "The draft post does not exist",
                $"Draft post with id: {draftPostId} does not exist"
            );
        }

        return draftPost.AuthorId;
    }

    public async Task Delete(DraftPost post) {
        _db.DraftPosts.Remove(post);
        await _db.SaveChangesAsync();
    }
}

file static class DraftPostsRepositoryExtensions
{
    public static IQueryable<DraftPost> OrderByPinnedThenSortOption(
        this IQueryable<DraftPost> query,
        bool putPinnedOnTop,
        DraftPostsSortOption sortOption
    ) {
        if (!putPinnedOnTop)
            return query.OrderBySortOption(sortOption);

        return query
            .OrderByDescending(p => p.IsPinned) 
            .ThenBySortOption(sortOption);
    }

    private static IOrderedQueryable<DraftPost> OrderBySortOption(
        this IQueryable<DraftPost> query,
        DraftPostsSortOption sortOption
    ) => sortOption switch {
        DraftPostsSortOption.Title => query.OrderBy(p => p.Title),
        DraftPostsSortOption.LastModified => query.OrderByDescending(p => p.LastModifiedAt),
        DraftPostsSortOption.LastCreated => query.OrderByDescending(p => p.CreatedAt),
        DraftPostsSortOption.OldestCreated => query.OrderBy(p => p.CreatedAt),
        _ => throw new ArgumentOutOfRangeException(
            $"Unexpected {nameof(sortOption)} value: {sortOption} in {nameof(OrderBySortOption)}"
        )
    };

    private static IOrderedQueryable<DraftPost> ThenBySortOption(
        this IOrderedQueryable<DraftPost> query,
        DraftPostsSortOption sortOption
    ) => sortOption switch {
        DraftPostsSortOption.Title => query.ThenBy(p => p.Title),
        DraftPostsSortOption.LastModified => query.ThenByDescending(p => p.LastModifiedAt),
        DraftPostsSortOption.LastCreated => query.ThenByDescending(p => p.CreatedAt),
        DraftPostsSortOption.OldestCreated => query.ThenBy(p => p.CreatedAt),
        _ => throw new ArgumentOutOfRangeException(
            $"Unexpected {nameof(sortOption)} value: {sortOption} in {nameof(ThenBySortOption)}"
        )
    };
}