using Microsoft.EntityFrameworkCore;
using SayPostMainService.Domain.common.interfaces.repositories;

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
                ORDER BY "Id"
                LIMIT @count
                """,
                new Npgsql.NpgsqlParameter("searchQuery", searchQuery),
                new Npgsql.NpgsqlParameter("count", count)
            )
            .ToArrayAsync();
}