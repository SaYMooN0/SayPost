using SayPostMainService.Domain.post_comment_aggregate;

namespace SayPostMainService.Api.contracts.post_comments;

public record class ListPostCommentsResponse(
    PostCommentResponse[] Comments
)
{
    public static ListPostCommentsResponse FromComments(IReadOnlyCollection<PostComment> comments) =>
        new(comments.Select(PostCommentResponse.FromComment).ToArray());
}