using SayPostMainService.Domain.common;
using SayPostMainService.Domain.common.post_aggregates_shared;
using SayPostMainService.Domain.common.post_aggregates_shared.post_content;
using SayPostMainService.Domain.draft_post_aggregate.events;
using SayPostMainService.Domain.published_post_aggregate.events;
using SayPostMainService.Domain.rules;
using SharedKernel.common.domain;
using SharedKernel.common.domain.ids;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;
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
    public bool IsPinned { get; private set; }

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
            content: PostContent.Default(),
            createdAt: timeProvider.Now,
            lastModifiedAt: timeProvider.Now
        );
        post.AddDomainEvent(new NewDraftPostCreatedEvent(post.Id, post.AuthorId));
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

    public ErrOrNothing UpdateTags(HashSet<PostTagId> newTags, IDateTimeProvider dateTimeProvider) {
        if (newTags.Count > PostsRules.MaxTagsForPostCount) {
            return PostsRules.TooManyTagsForPostSelectedErr(newTags.Count);
        }

        Tags = newTags;
        LastModifiedAt = dateTimeProvider.Now;
        return ErrOrNothing.Nothing;
    }

    public void Pin() => IsPinned = true;
    public void Unpin() => IsPinned = false;
    public void Delete() => AddDomainEvent(new DraftPostDeletedEvent(Id, AuthorId));
    public bool IsTitleDefault() => Title.ToString() == PostTitle.DefaultValue;
}