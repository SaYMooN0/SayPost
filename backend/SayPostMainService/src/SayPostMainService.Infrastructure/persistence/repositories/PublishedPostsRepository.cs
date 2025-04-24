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
        await _db.PublishedPostsPosts.AddAsync(post);
        await _db.SaveChangesAsync();
    }
}