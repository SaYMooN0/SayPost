using SayPostMainService.Domain.post_comment_aggregate;

namespace SayPostMainService.Api.contracts.post_comments;

public record class PostCommentResponse(
    string Id,
    string Content,
    DateTime CreatedAt,
    string AuthorId
)
{
    public static PostCommentResponse FromComment(PostComment comment) => new(
        comment.Id.ToString(),
        comment.Content,
        comment.CreatedAt,
        comment.AuthorId.ToString()
    );
}