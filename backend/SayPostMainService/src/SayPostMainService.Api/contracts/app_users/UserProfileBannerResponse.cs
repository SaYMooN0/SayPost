using SayPostMainService.Domain.app_user_aggregate;
using SayPostMainService.Domain.app_user_aggregate.profile_banner;

namespace SayPostMainService.Api.contracts.app_users;

public record UserProfileBannerResponse(
    float Scale,
    BannerDesign Design,
    BannerDesignVariant Variant,
    string[] Colors
)
{
    public static UserProfileBannerResponse FromProfileBanner(UserProfileBanner banner) => new(
        banner.Scale,
        banner.Design,
        banner.DesignVariant,
        banner.Colors.Select(c => c.ToString()).ToArray()
    );
}