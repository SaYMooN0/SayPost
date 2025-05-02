using SayPostMainService.Domain.post_tag_aggregate;

namespace SayPostMainService.Domain.common.interfaces.repositories;

public interface IPostTagsRepository
{
    Task<string[]> TagIdValuesWithSubstring(string searchQuery, int count);
    Task<PostTag[]> GetTagsWithIds(PostTagId[] tagIds);
    Task AddRange(PostTag[] tags);
    Task UpdateRange(PostTag[] tags);
}