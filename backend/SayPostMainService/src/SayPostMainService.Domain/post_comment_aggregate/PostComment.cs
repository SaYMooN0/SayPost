using SayPostMainService.Domain.post_comment_aggregate.events;
using SayPostMainService.Domain.published_post_aggregate.events;
using SharedKernel.common.domain;
using SharedKernel.common.domain.ids;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;
using SharedKernel.date_time_provider;

namespace SayPostMainService.Domain.post_comment_aggregate;

public class PostComment : AggregateRoot<PostCommentId>
{
    private PostComment() { }

    public string Content { get; }
    public PublishedPostId PostId { get; }
    public AppUserId AuthorId { get; }
    public DateTime CreatedAt { get; }

    private PostComment(
        PostCommentId id, string content, PublishedPostId postId, AppUserId authorId, DateTime createdAt
    ) {
        Id = id;
        Content = content;
        PostId = postId;
        AuthorId = authorId;
        CreatedAt = createdAt;
    }

    public const int MaxCommentLength = 1000;

    public static ErrOr<PostComment> CreateNew(
        string text,
        PublishedPostId postId,
        AppUserId authorId,
        IDateTimeProvider dateTimeProvider
    ) {
        if (string.IsNullOrEmpty(text)) {
            return ErrFactory.NoValue("Comment cannot be empty");
        }

        if (text.Length > MaxCommentLength) {
            return ErrFactory.NoValue(
                $"Comment text cannot be longer than {MaxCommentLength}",
                $"Current comment text length is {text.Length}"
            );
        }

        var comment = new PostComment(PostCommentId.CreateNew(), text, postId, authorId, dateTimeProvider.Now);
        comment.AddDomainEvent(new NewPostCommentCreatedEvent(comment.Id, postId, authorId));
        return comment;
    }
}