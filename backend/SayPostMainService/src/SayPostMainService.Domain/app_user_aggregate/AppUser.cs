using System.Collections.Immutable;
using SayPostMainService.Domain.app_user_aggregate.profile_banner;
using SayPostMainService.Domain.common;
using SayPostMainService.Domain.published_post_aggregate;
using SharedKernel.common.domain;
using SharedKernel.common.domain.ids;
using SharedKernel.common.errs;

namespace SayPostMainService.Domain.app_user_aggregate;

public class AppUser : AggregateRoot<AppUserId>
{
    private AppUser() { }
    private HashSet<DraftPostId> _draftPostIds { get; }
    private HashSet<PublishedPostId> _publishedPostIds { get; }
    public UserProfileBanner ProfileBanner { get; }

    public AppUser(AppUserId id) {
        Id = id;
        _draftPostIds = [];
        _publishedPostIds = [];
        ProfileBanner = UserProfileBanner.CreateNew();
    }

    public ImmutableHashSet<DraftPostId> DraftPostIds => _draftPostIds.ToImmutableHashSet();
    public ImmutableHashSet<PublishedPostId> PublishedPostIds => _publishedPostIds.ToImmutableHashSet();

    public void AddDraftPost(DraftPostId draftPostId) =>
        _draftPostIds.Add(draftPostId);

    public void RemoveDraftPost(DraftPostId draftPostId) =>
        _draftPostIds.Remove(draftPostId);

    public ErrOrNothing UpdateProfileBanner(
        float scale, BannerDesign design, BannerDesignVariant designVariant, HexColor[] colors
    ) =>
        ProfileBanner.Update(scale, design, designVariant, colors);
}