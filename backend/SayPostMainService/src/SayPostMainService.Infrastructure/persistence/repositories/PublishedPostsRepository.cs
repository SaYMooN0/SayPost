using Microsoft.EntityFrameworkCore;
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

    public Task<PublishedPost[]> QueryPosts(PostsQueryFilter filter) => _db.PublishedPosts
        .OrderByDescending(p => p.PublicationDate)
        .ToArrayAsync();
}

file static class PublishedPostsRepositoryExtensions { }