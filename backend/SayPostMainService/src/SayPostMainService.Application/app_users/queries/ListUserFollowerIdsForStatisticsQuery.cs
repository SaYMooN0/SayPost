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

public record class ListUserFollowerIdsForStatisticsQuery(AppUserId UserId)
    : IRequest<ErrOr<IReadOnlyCollection<AppUserId>>>;

internal class ListUserFollowerIdsQueryForStatisticsHandler :
    IRequestHandler<ListUserFollowerIdsForStatisticsQuery, ErrOr<IReadOnlyCollection<AppUserId>>>
{
    private readonly IUserFollowingsReadRepository _userFollowingsReadRepository;
    private readonly ICurrentActorProvider _currentActorProvider;
    private readonly IAppUsersRepository _appUsersRepository;

    public ListUserFollowerIdsQueryForStatisticsHandler(
        IUserFollowingsReadRepository userFollowingsReadRepository,
        ICurrentActorProvider currentActorProvider,
        IAppUsersRepository appUsersRepository
    ) {
        _userFollowingsReadRepository = userFollowingsReadRepository;
        _currentActorProvider = currentActorProvider;
        _appUsersRepository = appUsersRepository;
    }

    public async Task<ErrOr<IReadOnlyCollection<AppUserId>>> Handle(
        ListUserFollowerIdsForStatisticsQuery query, CancellationToken cancellationToken
    ) {
        AppUser? user = await _appUsersRepository.GetByIdAsNoTracking(query.UserId);
        if (user is null) {
            return ErrFactory.NotFound(
                "User not found", $"Could not get user followers. User {query.UserId} was not found"
            );
        }

        var viewerIdRes = _currentActorProvider.UserId;
        bool isAuthenticated = viewerIdRes.IsSuccess(out var viewerId);
        if (!isAuthenticated && user.StatisticsVisibilitySettings.FollowersOnlyForFollowers) {
            return ErrFactory.NoAccess(
                "Only user's followers can see this information. Please log into your account and follow this user"
            );
        }

        AppUserWithFollowingsData? userWithFollowers = await _userFollowingsReadRepository.GetUser(query.UserId);
        if (userWithFollowers is null) {
            return ErrFactory.NotFound(
                "User not found", $"Could not get user followers. User {query.UserId} was not found"
            );
        }

        if (
            viewerId == query.UserId
            || !user.StatisticsVisibilitySettings.FollowersOnlyForFollowers
            || userWithFollowers.IsFollowedBy(viewerId)
        ) {
            return userWithFollowers.FollowerIds;
        }

        return ErrFactory.NoAccess("Only user's followers can see this information");
    }
}