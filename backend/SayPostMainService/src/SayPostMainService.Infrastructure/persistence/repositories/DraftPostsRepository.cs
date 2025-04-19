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
        DraftPostsSortOption sortOption
    ) => await _db.DraftPosts
        .Where(p => p.AuthorId == userId)
        .OrderBySortOption(sortOption)
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
}

file static class DraftPostsRepositoryExtensions
{
    public static IQueryable<DraftPost> OrderBySortOption(
        this IQueryable<DraftPost> query, DraftPostsSortOption sortOption
    ) => sortOption switch {
        DraftPostsSortOption.Title => query.OrderBy(p => p.Title),
        DraftPostsSortOption.LastModified => query.OrderByDescending(p => p.LastModifiedAt),
        DraftPostsSortOption.LastCreated => query.OrderByDescending(p => p.CreatedAt),
        DraftPostsSortOption.OldestCreated => query.OrderBy(p => p.CreatedAt),
        _ => throw new ArgumentOutOfRangeException(
            $"Unexpected {nameof(sortOption)} value: {sortOption} in the {nameof(OrderBySortOption)}"
        )
    };
}