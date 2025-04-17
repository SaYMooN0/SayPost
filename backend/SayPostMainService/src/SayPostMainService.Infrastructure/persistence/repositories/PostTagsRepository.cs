using SayPostMainService.Domain.common.interfaces.repositories;

namespace SayPostMainService.Infrastructure.persistence.repositories;

internal class PostTagsRepository :IPostTagsRepository
{
    private MainDbContext _db;

    public PostTagsRepository(MainDbContext db) {
        _db = db;
    }

}