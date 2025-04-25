using SayPostMainService.Domain.draft_post_aggregate;

namespace SayPostMainService.Api.contracts.draft_posts;

internal record class DraftPostMainInfo(
    string Id,
    string Title,
    bool IsPinned,
    DateTime LastModifiedAt
)
{
    public static DraftPostMainInfo FromPost(DraftPost post) => new(
        post.Id.ToString(),
        post.Title.ToString(),
        post.IsPinned,
        post.LastModifiedAt
    );
}