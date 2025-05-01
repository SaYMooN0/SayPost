using Microsoft.EntityFrameworkCore;
using SayPostMainService.Domain.common;
using SayPostMainService.Domain.common.interfaces.repositories;
using SayPostMainService.Domain.published_post_aggregate;

namespace SayPostMainService.Infrastructure.persistence.repositories;

internal class PublishedPostsRepository : IPublishedPostsRepository
{
    private MainDbContext _db;

    public PublishedPostsRepository(MainDbContext db) {
        _db = db;
    }

    public async Task Add(PublishedPost post) {
        await _db.PublishedPosts.AddAsync(post);
        await _db.SaveChangesAsync();
    }

    public async Task<PublishedPost[]> QueryPostsWithComments(PostsQueryFilter filter) =>
        (await _db.PublishedPosts
            .AsNoTracking()
            .ApplyDateFilter(filter.DateFrom, filter.DateTo)
            .Include(u => EF.Property<ICollection<PostComment>>(u, "_comments"))
            .ToArrayAsync()
        )
        .ApplyTagFilter(filter.IncludeTags, filter.ExcludeTags)
        .OrderByDescending(p => p.PublicationDate)
        .ToArray();
}

file static class PublishedPostsRepositoryExtensions
{
    public static IQueryable<PublishedPost> ApplyDateFilter(
        this IQueryable<PublishedPost> query,
        DateTime? from,
        DateTime? to
    ) {
        if (from.HasValue) {
            query = query.Where(p => p.PublicationDate >= from.Value);
        }

        if (to.HasValue) {
            query = query.Where(p => p.PublicationDate <= to.Value);
        }

        return query;
    }

    public static IEnumerable<PublishedPost> ApplyTagFilter(
        this IEnumerable<PublishedPost> posts,
        HashSet<PostTagId> includeTags,
        HashSet<PostTagId> excludeTags
    ) {
        if (includeTags.Count > 0) {
            posts = posts.Where(p => p.Tags.Any(t => includeTags.Contains(t)));
        }

        if (excludeTags.Count > 0) {
            posts = posts.Where(p => p.Tags.All(t => !excludeTags.Contains(t)));
        }

        return posts;
    }
}