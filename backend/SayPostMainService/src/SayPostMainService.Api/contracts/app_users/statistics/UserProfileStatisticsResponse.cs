using SayPostMainService.Application.app_users.queries;

namespace SayPostMainService.Api.contracts.app_users.statistics;

public record UserProfileStatisticsResponse(
    StatisticsFieldValue PublishedPosts,
    StatisticsFieldValue Followers,
    StatisticsFieldValue Followings,
    StatisticsFieldValue CommentsLeft,
    StatisticsFieldValue LikedPosts
)
{
    public static UserProfileStatisticsResponse FromVm(UserProfileStatisticsVm vm) => new(
        vm.PublishedPosts,
        vm.Followers,
        vm.Followings,
        vm.CommentsLeft,
        vm.LikedPosts
    );
}