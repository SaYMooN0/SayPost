using SayPostMainService.Domain.common;
using SayPostMainService.Domain.common.post_aggregates_shared;
using SayPostMainService.Domain.common.post_aggregates_shared.post_content;
using SayPostMainService.Domain.draft_post_aggregate;
using SayPostMainService.Domain.published_post_aggregate.events;
using SharedKernel.common.domain;
using SharedKernel.common.domain.ids;
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
    public ICollection<PostComment> _comments { get; }

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
        _comments = new List<PostComment>();
    }

    public static PublishedPost CreateFromDraft(DraftPost draftPost, IDateTimeProvider dateTimeProvider) {
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

    public IReadOnlyCollection<PostComment> Comments => _comments.ToList();

    public void AddComment(PostComment comment) {
        _comments.Add(comment);
        AddDomainEvent(new NewCommentToPostAddedEvent(
            this.AuthorId,
            this.Title,
            comment.AuthorId
        ));
    }
}