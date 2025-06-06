using SayPostMainService.Domain.app_user_aggregate.profile_banner;
using SayPostMainService.Domain.common;
using SharedKernel.common.domain;
using SharedKernel.common.domain.ids;
using SharedKernel.common.errs;

namespace SayPostMainService.Domain.app_user_aggregate;

public class AppUser : AggregateRoot<AppUserId>
{
    private AppUser() { }
    private HashSet<DraftPostId> _draftPostIds { get; }
    private HashSet<PublishedPostId> _publishedPostIds { get; }
    private HashSet<PublishedPostId> _likedPostIds { get; }
    private HashSet<PostCommentId> _leftCommentIds { get; }
    public UserProfileBanner ProfileBanner { get; }
    public StatisticsVisibilitySettings StatisticsVisibilitySettings { get; private set; }

    public AppUser(AppUserId id) {
        Id = id;
        _draftPostIds = [];
        _publishedPostIds = [];
        _likedPostIds = [];
        _leftCommentIds = [];
        ProfileBanner = UserProfileBanner.CreateNew();
        StatisticsVisibilitySettings = StatisticsVisibilitySettings.Default;
    }

    public int PublishedPostsCount => _publishedPostIds.Count;
    public int LikedPostsCount => _likedPostIds.Count;
    public IReadOnlyCollection<PublishedPostId> LikedPostIds() => _likedPostIds;
    public int LeftCommentsCount => _leftCommentIds.Count;

    public void AddDraftPost(DraftPostId draftPostId) => _draftPostIds.Add(draftPostId);

    public void RemoveDraftPost(DraftPostId draftPostId) => _draftPostIds.Remove(draftPostId);
    public void AddPublishedPost(PublishedPostId postId) => _publishedPostIds.Add(postId);

    public void AddComment(PostCommentId commentId) => _leftCommentIds.Add(commentId);

    public void AddLikedPost(PublishedPostId publishedPostId) => _likedPostIds.Add(publishedPostId);
    public bool RemoveLikedPost(PublishedPostId publishedPostId) => _likedPostIds.Remove(publishedPostId);

    public ErrOrNothing UpdateProfileBanner(
        float scale, BannerDesign design, BannerDesignVariant designVariant, HexColor[] colors
    ) => ProfileBanner.Update(scale, design, designVariant, colors);

    public void UpdateStatisticsVisibilitySettings(StatisticsVisibilitySettings newVal) {
        StatisticsVisibilitySettings = newVal;
    }

    

}