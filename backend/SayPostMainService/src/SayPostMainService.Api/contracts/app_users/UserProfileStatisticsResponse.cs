using SayPostMainService.Application.app_users.queries;

namespace SayPostMainService.Api.contracts.app_users;

public record UserProfileStatisticsResponse(
    int PublishedPostsCount,
    int FollowersCount,
    int FollowingsCount,
    int CommentsLeftCount,
    int LikedPostsCount
)
{
    public static UserProfileStatisticsResponse Create(UserFullProfileDataVm vm) => new(
        vm.PublishedPostsCount,
        vm.FollowersCount,
        vm.FollowingsCount,
        vm.CommentsCount,
        vm.LikedPostsCount
    );
}