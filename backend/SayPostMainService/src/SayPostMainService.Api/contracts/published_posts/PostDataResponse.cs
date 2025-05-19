using SayPostMainService.Domain.common.post_aggregates_shared.post_content;
using SayPostMainService.Domain.published_post_aggregate;
using SharedKernel.common.domain.ids;

namespace SayPostMainService.Api.contracts.published_posts;

public record class PostDataResponse(
    string Id,
    string Title,
    PostContent Content,
    DateTime PublicationDate,
    string[] Tags,
    string AuthorId,
    int LikesCount,
    bool? IsLikedByActor
)
{
    public static PostDataResponse FromPost(PublishedPost post, AppUserId? actorId) => new(
        post.Id.ToString(),
        post.Title.ToString(),
        post.Content,
        post.PublicationDate,
        post.Tags.Select(t => t.ToString()).ToArray(),
        post.AuthorId.ToString(),
        post.LikesCount,
        actorId is null ? null : post.IsLikedBy(actorId)
    );
}