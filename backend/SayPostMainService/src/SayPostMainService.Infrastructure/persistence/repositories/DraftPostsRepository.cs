using SayPostMainService.Domain.common.interfaces.repositories;
using SayPostMainService.Domain.draft_post_aggregate;

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
}