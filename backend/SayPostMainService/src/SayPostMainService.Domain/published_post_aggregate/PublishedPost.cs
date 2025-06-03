using SayPostMainService.Domain.app_user_aggregate;
using SayPostMainService.Domain.common;
using SayPostMainService.Domain.common.post_aggregates_shared;
using SayPostMainService.Domain.common.post_aggregates_shared.post_content;
using SayPostMainService.Domain.draft_post_aggregate;
using SayPostMainService.Domain.post_comment_aggregate;
using SayPostMainService.Domain.post_comment_aggregate.events;
using SayPostMainService.Domain.published_post_aggregate.events;
using SharedKernel.common.domain;
using SharedKernel.common.domain.ids;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;
using SharedKernel.date_time_provider;

namespace SayPostMainService.Domain.published_post_aggregate;

public class PublishedPost : AggregateRoot<PublishedPostId>
{
    private PublishedPost() { }

    public AppUserId AuthorId { get; }
    public PostTitle Title { get; }
    public PostContent Content { get; }
    public DateTime PublicationDate { get; }
    public HashSet<PostTagId> Tags { get; }
    private HashSet<AppUserId> _likedByUserIds { get; }
    public int CommentsCount { get; private set; }


    public PublishedPost(
        PublishedPostId id, AppUserId authorId,
        PostTitle title, PostContent content,
        DateTime publicationDate, HashSet<PostTagId> tags
    ) {
        Id = id;
        AuthorId = authorId;
        Title = title;
        Content = content;
        PublicationDate = publicationDate;
        Tags = tags;
        _likedByUserIds = [];
        CommentsCount = 0;
    }

    public static ErrOr<PublishedPost> TryCreateFromDraft(DraftPost draftPost, IDateTimeProvider dateTimeProvider) {
        if (draftPost.IsTitleDefault()) {
            return ErrFactory.Conflict(
                $"Cannot publish a post with default title: {draftPost.Title}",
                "Please change post title before publishing"
            );
        }

        PublishedPost publishedPost = new(
            PublishedPostId.CreateNew(), draftPost.AuthorId,
            draftPost.Title, draftPost.Content,
            dateTimeProvider.Now, draftPost.Tags
        );
        publishedPost.AddDomainEvent(new NewPublishedPostCreatedEvent(
            publishedPost.Id,
            publishedPost.AuthorId,
            publishedPost.Tags.ToArray()
        ));
        return publishedPost;
    }


    public void AddComment(PostCommentId commentId, AppUserId commentAuthorId) {
        CommentsCount = CommentsCount + 1;
        AddDomainEvent(new NewCommentToPostAddedEvent(commentId, this.AuthorId, this.Title, commentAuthorId));
    }

    public int LikesCount => _likedByUserIds.Count;
    public bool IsLikedBy(AppUserId userId) => _likedByUserIds.Contains(userId);

    public ErrOrNothing Like(AppUserId userId) {
        if (IsLikedBy(userId)) {
            return ErrFactory.Conflict("This post is already liked by this user");
        }

        _likedByUserIds.Add(userId);
        AddDomainEvent(new PostLikedEvent(this.Id, this.AuthorId, userId));
        return ErrOrNothing.Nothing;
    }

    public ErrOrNothing Unlike(AppUserId userId) {
        if (!IsLikedBy(userId)) {
            return ErrFactory.Conflict("This post is not liked by this user");
        }

        _likedByUserIds.Remove(userId);
        AddDomainEvent(new PostUnlikedEvent(this.Id, userId));
        return ErrOrNothing.Nothing;
    }
}