using SayPostMainService.Domain.common;
using SayPostMainService.Domain.draft_post_aggregate.events;
using SharedKernel.common.domain;
using SharedKernel.common.domain.ids;
using SharedKernel.date_time_provider;

namespace SayPostMainService.Domain.draft_post_aggregate;

public class DraftPost : AggregateRoot<DraftPostId>
{
    private DraftPost() { }
    public AppUserId AuthorId { get; }
    public DraftPostTitle Title { get; private set; }
    public DraftPostContent Content { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime LastModifiedAt { get; private set; }
    public HashSet<PostTagId> Tags { get; private set; }

    private DraftPost(
        AppUserId authorId, DraftPostTitle title, DraftPostContent content, DateTime createdAt, DateTime lastModifiedAt
    ) {
        AuthorId = authorId;
        Title = title;
        Content = content;
        CreatedAt = createdAt;
        LastModifiedAt = lastModifiedAt;
        Tags = [];
    }

    public static DraftPost CreateNew(AppUserId authorId, IDateTimeProvider timeProvider) {
        DraftPost post = new(
            authorId: authorId,
            title: DraftPostTitle.CreateNew(),
            content: DraftPostContent.CreateNew(),
            createdAt: timeProvider.Now,
            lastModifiedAt: timeProvider.Now
        );
        post._domainEvents.Add(new NewDraftPostCreatedEvent(post.Id, post.AuthorId));
        return post;
    }

    public void UpdateTitle(DraftPostTitle newTitle, IDateTimeProvider timeProvider) {
        Title = newTitle;
        LastModifiedAt = timeProvider.Now;
    }

    public void UpdateContent(DraftPostContent newContent, IDateTimeProvider dateTimeProvider) {
        Content = newContent;
        LastModifiedAt = dateTimeProvider.Now;
    }
}