using SharedKernel.common.domain.entity;
using SharedKernel.common.domain.ids;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;
using SharedKernel.date_time_provider;

namespace SayPostMainService.Domain.published_post_aggregate;

public class PostComment : Entity<PostCommentId>
{
    private PostComment() { }

    public string Content { get; }
    public AppUserId AuthorId { get; }
    public DateTime CreatedAt { get; }

    private PostComment(PostCommentId id, string content, DateTime createdAt, AppUserId authorId) {
        Id = id;
        Content = content;
        CreatedAt = createdAt;
        AuthorId = authorId;
    }

    public const int MaxCommentLength = 1000;

    public static ErrOr<PostComment> CreateNew(string text, AppUserId authorId, IDateTimeProvider dateTimeProvider) {
        if (string.IsNullOrEmpty(text)) {
            return ErrFactory.NoValue("Comment cannot be empty");
        }

        if (text.Length > MaxCommentLength) {
            return ErrFactory.NoValue(
                $"Comment text cannot be longer than {MaxCommentLength}",
                $"Current comment text length is {text.Length}"
            );
        }

        return new PostComment(PostCommentId.CreateNew(), text, dateTimeProvider.Now, authorId);
    }
}