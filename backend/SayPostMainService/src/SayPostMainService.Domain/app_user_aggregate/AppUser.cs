using System.Collections.Immutable;
using SayPostMainService.Domain.common;
using SayPostMainService.Domain.published_post_aggregate;
using SharedKernel.common.domain;
using SharedKernel.common.domain.ids;

namespace SayPostMainService.Domain.app_user_aggregate;

public class AppUser : AggregateRoot<AppUserId>
{
    private AppUser() { }
    private HashSet<DraftPostId> _draftPostIds { get; }
    private HashSet<PublishedPostId> _publishedPostIds { get; }

    public AppUser(AppUserId id) {
        Id = id;
        _draftPostIds = [];
        _publishedPostIds = [];
    }

    public ImmutableHashSet<DraftPostId> DraftPostIds => _draftPostIds.ToImmutableHashSet();
    public ImmutableHashSet<PublishedPostId> PublishedPostIds => _publishedPostIds.ToImmutableHashSet();

    public void AddDraftPost(DraftPostId draftPostId) {
        _draftPostIds.Add(draftPostId);
    }
}