using SayPostMainService.Domain.common;
using SharedKernel.common.domain;
using SharedKernel.common.domain.ids;
using SharedKernel.date_time_provider;

namespace SayPostMainService.Domain.draft_post_aggregate;

public class DraftPost : AggregateRoot<DraftPostId>
{
    private DraftPost() { }
    private AppUserId AuthorId { get; }
    public DraftPostTitle Title { get; private set; }
    public DraftPostContent Content { get; private set; }
    public DateTime StartedAt { get; }
    public DateTime LastModifiedAt { get; private set; }

    private DraftPost(AppUserId authorId, DraftPostTitle title, DraftPostContent content, DateTime startedAt,
        DateTime lastModifiedAt) {
        AuthorId = authorId;
        Title = title;
        Content = content;
        StartedAt = startedAt;
        LastModifiedAt = lastModifiedAt;
    }

    public static DraftPost CreateNew(AppUserId authorId, IDateTimeProvider timeProvider) => new(
        authorId: authorId,
        title: DraftPostTitle.CreateNew(),
        content: DraftPostContent.CreateNew(),
        startedAt: timeProvider.Now,
        lastModifiedAt: timeProvider.Now
    );
}