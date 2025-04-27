using SayPostMainService.Domain.draft_post_aggregate;

namespace SayPostMainService.Api.contracts.draft_posts.update_tags;

public record DraftPostTagsUpdatedResponse(
    DateTime NewLastModified,
    string[] NewTags
)
{
    public static DraftPostTagsUpdatedResponse FromDraftPost(DraftPost draftPost) => new(
        draftPost.LastModifiedAt,
        draftPost.Tags.Select(t => t.ToString()).ToArray()
    );
}