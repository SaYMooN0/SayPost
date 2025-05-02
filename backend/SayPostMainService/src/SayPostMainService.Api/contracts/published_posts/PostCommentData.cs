using SayPostMainService.Domain.published_post_aggregate;

namespace SayPostMainService.Api.contracts.published_posts;

public record class PostCommentData(
    string Id,
    string Content,
    DateTime CreatedAt,
    string AuthorId
)
{
    public static PostCommentData FromComment(PostComment comment) => new(
        comment.Id.ToString(),
        comment.Content,
        comment.CreatedAt,
        comment.AuthorId.ToString()
    );
}