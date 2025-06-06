using SayPostMainService.Application.app_users.queries;

namespace SayPostMainService.Api.contracts.app_users;

public record UserProfileDataResponse(
    string UserId,
    bool IsFollowedByViewer,
    UserProfileBannerResponse ProfileBanner
)
{
    public static UserProfileDataResponse Create(UserFullProfileDataVm vm) => new(
        vm.UserId,
        vm.IsFollowedByViewer,
        UserProfileBannerResponse.FromProfileBanner(vm.ProfileBanner)
    );
}