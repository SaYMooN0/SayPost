using SayPostMainService.Domain.draft_post_aggregate;

namespace SayPostMainService.Api.contracts.draft_posts;

public record class DraftPostFullInfoResponse(
    string Id,
    string Title,
    string Content,
    DateTime CreatedAt,
    string[] Tags
)
{
    public static DraftPostFullInfoResponse FromDraftPost(DraftPost draftPost) => new(
        draftPost.Id.ToString(),
        draftPost.Title.ToString(),
        draftPost.Content.ToString(),
        draftPost.CreatedAt,
        draftPost.Tags.Select(t => t.ToString()).ToArray()
    );
}