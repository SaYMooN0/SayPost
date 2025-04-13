using SayPostMainService.Domain.draft_post_aggregate;

namespace SayPostMainService.Api.contracts.draft_posts;

internal record class DraftPostMainInfo(
    string Title,
    DateTime LastModified
)
{
    public static DraftPostMainInfo FromPost(DraftPost post) => new(
        post.Title.ToString(),
        post.LastModifiedAt
    );
}