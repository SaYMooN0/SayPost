using SayPostMainService.Domain.published_post_aggregate;

namespace SayPostMainService.Api.contracts.published_posts;

public record class PublishedPostPreviewResponse(
    string Id,
    string Title,
    DateTime PublicationDate,
    string[] Tags,
    int CommentsCount,
    int LikesCount,
    string AuthorId
)
{
    public static PublishedPostPreviewResponse FromPost(PublishedPost post) => new(
        post.Id.ToString(),
        post.Title.ToString(),
        post.PublicationDate,
        post.Tags.Select(t => t.ToString()).ToArray(),
        post.CommentsCount,
        post.LikesCount,
        post.AuthorId.ToString()
    );
}