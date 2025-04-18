using SayPostMainService.Domain.common;
using SayPostMainService.Domain.common.post_aggregates_shared;
using SayPostMainService.Domain.draft_post_aggregate.events;
using SharedKernel.common.domain;
using SharedKernel.common.domain.ids;
using SharedKernel.date_time_provider;

namespace SayPostMainService.Domain.draft_post_aggregate;

public class DraftPost : AggregateRoot<DraftPostId>
{
    private DraftPost() { }
    public AppUserId AuthorId { get; }
    public PostTitle Title { get; private set; }
    public PostContent Content { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime LastModifiedAt { get; private set; }
    public HashSet<PostTagId> Tags { get; private set; }

    private DraftPost(
        DraftPostId id, AppUserId authorId,
        PostTitle title, PostContent content,
        DateTime createdAt, DateTime lastModifiedAt
    ) {
        Id = id;
        AuthorId = authorId;
        Title = title;
        Content = content;
        CreatedAt = createdAt;
        LastModifiedAt = lastModifiedAt;
        Tags = [];
    }

    public static DraftPost CreateNew(AppUserId authorId, IDateTimeProvider timeProvider) {
        DraftPost post = new(
            id: DraftPostId.CreateNew(),
            authorId: authorId,
            title: PostTitle.CreateNew(),
            content: PostContent.CreateNew(),
            createdAt: timeProvider.Now,
            lastModifiedAt: timeProvider.Now
        );
        post._domainEvents.Add(new NewDraftPostCreatedEvent(post.Id, post.AuthorId));
        return post;
    }

    public void UpdateTitle(PostTitle newTitle, IDateTimeProvider timeProvider) {
        Title = newTitle;
        LastModifiedAt = timeProvider.Now;
    }

    public void UpdateContent(PostContent newContent, IDateTimeProvider dateTimeProvider) {
        Content = newContent;
        LastModifiedAt = dateTimeProvider.Now;
    }
}