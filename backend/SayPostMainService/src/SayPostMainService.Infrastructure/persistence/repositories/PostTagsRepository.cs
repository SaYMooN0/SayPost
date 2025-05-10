using Microsoft.EntityFrameworkCore;
using SayPostMainService.Domain.common;
using SayPostMainService.Domain.common.interfaces.repositories;
using SayPostMainService.Domain.post_tag_aggregate;

namespace SayPostMainService.Infrastructure.persistence.repositories;

internal class PostTagsRepository : IPostTagsRepository
{
    private MainDbContext _db;

    public PostTagsRepository(MainDbContext db) {
        _db = db;
    }

    public Task<string[]> TagIdValuesWithSubstring(string searchQuery, int count) =>
        _db.Database
            .SqlQueryRaw<string>(
                """
                SELECT "Id"
                FROM "PostTags"
                WHERE "Id" LIKE '%' || @searchQuery || '%'
                  AND "Id" <> @searchQuery
                ORDER BY "Id"
                LIMIT @count
                """,
                new Npgsql.NpgsqlParameter("searchQuery", searchQuery),
                new Npgsql.NpgsqlParameter("count", count)
            )
            .ToArrayAsync();

    public async Task<PostTag[]> GetTagsWithIds(PostTagId[] tagIds) {
        var rawIds = tagIds.Select(id => id).ToArray();
        return await _db.PostTags
            .Where(t => rawIds.Contains(t.Id))
            .ToArrayAsync();
    }

    public async Task AddRange(PostTag[] tags) {
        await _db.PostTags.AddRangeAsync(tags);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateRange(PostTag[] tags) {
        _db.PostTags.UpdateRange(tags);
        await _db.SaveChangesAsync();
    }
}