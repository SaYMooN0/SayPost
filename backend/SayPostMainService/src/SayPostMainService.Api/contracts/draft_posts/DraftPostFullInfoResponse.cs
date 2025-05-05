using SayPostMainService.Domain.common.post_aggregates_shared.post_content;
using SayPostMainService.Domain.draft_post_aggregate;

namespace SayPostMainService.Api.contracts.draft_posts;

public record class DraftPostFullInfoResponse(
    string Id,
    string Title,
    bool IsPinned,
    PostContent Content,
    DateTime CreatedAt,
    DateTime LastModifiedAt,
    string[] Tags
)
{
    public static DraftPostFullInfoResponse FromDraftPost(DraftPost draftPost) => new(
        draftPost.Id.ToString(),
        draftPost.Title.ToString(),
        draftPost.IsPinned,
        draftPost.Content,
        draftPost.CreatedAt,
        draftPost.LastModifiedAt,
        draftPost.Tags.Select(t => t.ToString()).ToArray()
    );
}