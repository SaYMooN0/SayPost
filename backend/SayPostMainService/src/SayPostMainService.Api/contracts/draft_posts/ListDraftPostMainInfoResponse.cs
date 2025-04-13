using SayPostMainService.Domain.draft_post_aggregate;

namespace SayPostMainService.Api.contracts.draft_posts;

internal record class ListDraftPostMainInfoResponse(
    DraftPostMainInfo[] Posts
)
{
    public static ListDraftPostMainInfoResponse FromPosts(IEnumerable<DraftPost> posts) => new(
        posts
            .Select(DraftPostMainInfo.FromPost)
            .ToArray()
    );
}