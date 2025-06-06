using FollowingsQueryLib.entities;
using FollowingsQueryLib.repository;
using MediatR;
using SayPostMainService.Application.interfaces;
using SayPostMainService.Domain.app_user_aggregate;
using SayPostMainService.Domain.common.interfaces.repositories;
using SharedKernel.common.domain.ids;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Application.app_users.queries;

public record class GetUserProfileStatisticsQuery(AppUserId UserId) : IRequest<ErrOr<UserProfileStatisticsVm>>;

public record UserProfileStatisticsVm(
    StatisticsFieldValue PublishedPosts,
    StatisticsFieldValue Followers,
    StatisticsFieldValue Followings,
    StatisticsFieldValue CommentsLeft,
    StatisticsFieldValue LikedPosts
);

internal class GetUserProfileStatisticsQueryHandler :
    IRequestHandler<GetUserProfileStatisticsQuery, ErrOr<UserProfileStatisticsVm>>
{
    private readonly IAppUsersRepository _appUsersRepository;
    private readonly ICurrentActorProvider _currentActorProvider;
    private readonly IUserFollowingsReadRepository _userFollowingsReadRepository;

    public GetUserProfileStatisticsQueryHandler(
        IAppUsersRepository appUsersRepository,
        ICurrentActorProvider currentActorProvider,
        IUserFollowingsReadRepository userFollowingsReadRepository
    ) {
        _appUsersRepository = appUsersRepository;
        _currentActorProvider = currentActorProvider;
        _userFollowingsReadRepository = userFollowingsReadRepository;
    }

    public async Task<ErrOr<UserProfileStatisticsVm>> Handle(
        GetUserProfileStatisticsQuery query, CancellationToken cancellationToken
    ) {
        var user = await _appUsersRepository.GetWithBannerAsNoTracking(query.UserId);
        if (user is null) {
            return ErrFactory.NotFound($"User with id {query.UserId} was not found");
        }

        AppUserWithFollowingsData? followingsData = await _userFollowingsReadRepository.GetUser(query.UserId);
        if (followingsData is null) {
            return ErrFactory.NotFound(
                "Could not get user followings state", $"User {query.UserId} was not found"
            );
        }

        bool isViewerTheFollowerOrTheUser = false;
        if (_currentActorProvider.UserId.IsSuccess(out var viewerId)) {
            isViewerTheFollowerOrTheUser = viewerId == query.UserId || followingsData.IsFollowedBy(viewerId);
        }

        return isViewerTheFollowerOrTheUser
                ? ShowWithoutHiding(user, followingsData)
                : ShowWithHiding(user, followingsData);
    }

    private static UserProfileStatisticsVm ShowWithoutHiding(
        AppUser user, AppUserWithFollowingsData userFollowingsData
    ) => new(
        StatisticsFieldValue.NotHidden(user.PublishedPostsCount),
        StatisticsFieldValue.NotHidden(userFollowingsData.FollowersCount),
        StatisticsFieldValue.NotHidden(userFollowingsData.FollowingsCount),
        StatisticsFieldValue.NotHidden(user.LeftCommentsCount),
        StatisticsFieldValue.NotHidden(user.LikedPostsCount)
    );

    private static UserProfileStatisticsVm ShowWithHiding(
        AppUser user,
        AppUserWithFollowingsData userFollowingsData
    )
    {
        var visibility = user.StatisticsVisibilitySettings;

        return new UserProfileStatisticsVm(
            visibility.PublishedPostsOnlyForFollowers
                ? StatisticsFieldValue.Hidden
                : StatisticsFieldValue.NotHidden(user.PublishedPostsCount),

            visibility.FollowersOnlyForFollowers
                ? StatisticsFieldValue.Hidden
                : StatisticsFieldValue.NotHidden(userFollowingsData.FollowersCount),

            visibility.FollowingOnlyForFollowers
                ? StatisticsFieldValue.Hidden
                : StatisticsFieldValue.NotHidden(userFollowingsData.FollowingsCount),

            visibility.LeftCommentsOnlyForFollowers
                ? StatisticsFieldValue.Hidden
                : StatisticsFieldValue.NotHidden(user.LeftCommentsCount),

            visibility.LikedPostsOnlyForFollowers
                ? StatisticsFieldValue.Hidden
                : StatisticsFieldValue.NotHidden(user.LikedPostsCount)
        );
    }

}

public class StatisticsFieldValue
{
    public bool IsHidden { get; }
    public int? Value { get; }

    private StatisticsFieldValue(bool isHidden, int? value) {
        IsHidden = isHidden;
        Value = value;
    }

    public static StatisticsFieldValue Hidden => new(true, null);
    public static StatisticsFieldValue NotHidden(int val) => new(false, val);
}