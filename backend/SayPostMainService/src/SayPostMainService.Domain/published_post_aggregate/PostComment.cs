using SharedKernel.common.domain.entity;
using SharedKernel.common.domain.ids;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Domain.published_post_aggregate;

public class PostComment : Entity<PostCommentId>
{
    private PostComment() { }

    public string Text { get; }
    public AppUserId AuthorId { get; }

    public PostComment(PostCommentId id, string text, AppUserId authorId) {
        Id = id;
        Text = text;
        AuthorId = authorId;
    }

    public const int MaxCommentLength = 1000;

    public ErrOr<PostComment> CreateNew(string text, AppUserId authorId) {
        if (string.IsNullOrEmpty(text)) {
            return ErrFactory.NoValue("Comment cannot be empty");
        }

        if (text.Length > MaxCommentLength) {
            return ErrFactory.NoValue(
                $"Comment text cannot be longer than {MaxCommentLength}",
                $"Current comment text length is {text.Length}"
            );
        }

        return new PostComment(PostCommentId.CreateNew(), text, authorId);
    }
}