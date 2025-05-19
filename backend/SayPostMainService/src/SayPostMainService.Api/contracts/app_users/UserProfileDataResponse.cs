using SayPostMainService.Domain.app_user_aggregate;

namespace SayPostMainService.Api.contracts.app_users;

public record UserProfileDataResponse(
    string UserId,
    UserProfileBannerResponse ProfileBanner
)
{
    public static UserProfileDataResponse FromUser(AppUser user) => new(
        user.Id.ToString(),
        UserProfileBannerResponse.FromProfileBanner(user.ProfileBanner)
    );
}


