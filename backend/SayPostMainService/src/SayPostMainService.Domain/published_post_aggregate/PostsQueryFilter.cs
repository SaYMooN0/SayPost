using SayPostMainService.Domain.common;
using SayPostMainService.Domain.post_tag_aggregate;

namespace SayPostMainService.Domain.published_post_aggregate;

public record class PostsQueryFilter(
    DateTime? DateFrom,
    DateTime? DateTo,
    HashSet<PostTagId> IncludeTags,
    HashSet<PostTagId> ExcludeTags
)
{
    public const int MaxTagsCount = 100;
}