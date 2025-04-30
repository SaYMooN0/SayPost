namespace SayPostMainService.Domain.published_post_aggregate;

public record class PostsQueryFilter(
    DateTime? DateFrom,
    DateTime? DateTo,
    string[] IncludeTags
);