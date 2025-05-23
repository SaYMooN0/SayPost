using SayPostMainService.Application.app_users.queries;
using SayPostMainService.Domain.app_user_aggregate;

namespace SayPostMainService.Api.contracts.app_users;

public record UserProfileDataResponse(
    string UserId,
    bool IsFollowedByViewer,
    UserProfileBannerResponse ProfileBanner,
    UserProfileStatisticsResponse Statistics
)
{
    public static UserProfileDataResponse Create(UserFullProfileDataVm vm) => new(
        vm.UserId,
        vm.IsFollowedByViewer,
        UserProfileBannerResponse.FromProfileBanner(vm.ProfileBanner),
        UserProfileStatisticsResponse.Create(vm)
    );
}