using FollowingsQueryLib.repository;
using MediatR;
using SayPostMainService.Application.interfaces;
using SayPostMainService.Domain.app_user_aggregate;
using SayPostMainService.Domain.common.interfaces.repositories;
using SharedKernel.common.domain.ids;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Application.app_users.queries;

public record class GetUserFullProfileDataQuery(AppUserId UserId) : IRequest<ErrOr<UserFullProfileDataVm>>;

public record class UserFullProfileDataVm(
    string UserId,
    bool IsFollowedByViewer,
    UserProfileBanner ProfileBanner
);

internal class GetUserFullProfileDataQueryHandler :
    IRequestHandler<GetUserFullProfileDataQuery, ErrOr<UserFullProfileDataVm>>
{
    private readonly IAppUsersRepository _appUsersRepository;
    private readonly ICurrentActorProvider _currentActorProvider;
    private readonly IUserFollowingsReadRepository _userFollowingsReadRepository;

    public GetUserFullProfileDataQueryHandler(
        IAppUsersRepository appUsersRepository,
        ICurrentActorProvider currentActorProvider,
        IUserFollowingsReadRepository userFollowingsReadRepository
    ) {
        _appUsersRepository = appUsersRepository;
        _currentActorProvider = currentActorProvider;
        _userFollowingsReadRepository = userFollowingsReadRepository;
    }

    public async Task<ErrOr<UserFullProfileDataVm>> Handle(
        GetUserFullProfileDataQuery query, CancellationToken cancellationToken
    ) {
        var user = await _appUsersRepository.GetWithBannerAsNoTracking(query.UserId);
        if (user is null) {
            return ErrFactory.NotFound($"User with id {query.UserId} was not found");
        }

        var followingsData = await _userFollowingsReadRepository.GetUser(query.UserId);
        if (followingsData is null) {
            return ErrFactory.NotFound(
                "Could not get user followings state", $"User {query.UserId} was not found"
            );
        }

        bool doesViewerFollow = false;
        var viewerIdRes = _currentActorProvider.UserId;
        if (viewerIdRes.IsSuccess(out var viewerId) && viewerId != query.UserId) {
            doesViewerFollow = followingsData.IsFollowedBy(viewerId);
        }

        return new UserFullProfileDataVm(
            user.Id.ToString(),
            doesViewerFollow,
            user.ProfileBanner
        );
    }
}