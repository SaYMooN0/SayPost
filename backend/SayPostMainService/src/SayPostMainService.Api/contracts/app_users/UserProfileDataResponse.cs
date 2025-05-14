using SayPostMainService.Domain.app_user_aggregate;
using SayPostMainService.Domain.app_user_aggregate.profile_banner;

namespace SayPostMainService.Api.contracts.app_users;

public record UserProfileDataResponse(
    string UserId,
    UserProfileBannerResponseData ProfileBanner
)
{
    public static UserProfileDataResponse FromUser(AppUser user) => new(
        user.Id.ToString(),
        UserProfileBannerResponseData.FromProfileBanner(user.ProfileBanner)
    );
}


public record UserProfileBannerResponseData(
    float Scale,
    BannerDesign Design,
    BannerDesignVariant Variant,
    string[] Colors
)
{
    public static UserProfileBannerResponseData FromProfileBanner(UserProfileBanner banner) => new(
        banner.Scale,
        banner.Design,
        banner.DesignVariant,
        banner.Colors.Select(c => c.ToString()).ToArray()
    );
}