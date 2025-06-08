using FollowingsQueryLib.repository;
using MediatR;
using SayPostMainService.Application.interfaces;
using SayPostMainService.Domain.app_user_aggregate;
using SayPostMainService.Domain.common.interfaces.repositories;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Application.app_users.queries;

public record class GetStatisticsVisibilitySettingsQuery() : IRequest<ErrOr<StatisticsVisibilitySettings>>;

internal class GetStatisticsVisibilitySettingsQueryHandler :
    IRequestHandler<GetStatisticsVisibilitySettingsQuery
        , ErrOr<StatisticsVisibilitySettings>>
{
    private readonly IAppUsersRepository _appUsersRepository;
    private readonly ICurrentActorProvider _currentActorProvider;

    public GetStatisticsVisibilitySettingsQueryHandler(
        IAppUsersRepository appUsersRepository,
        ICurrentActorProvider currentActorProvider
    ) {
        _appUsersRepository = appUsersRepository;
        _currentActorProvider = currentActorProvider;
    }

    public async Task<ErrOr<StatisticsVisibilitySettings>> Handle(
        GetStatisticsVisibilitySettingsQuery query, CancellationToken cancellationToken
    ) {
        var userId = _currentActorProvider.UserId.AsSuccess();
        var user = await _appUsersRepository.GetWithBannerAsNoTracking(userId);
        if (user is null) {
            return ErrFactory.NotFound($"User with id {userId} was not found");
        }

        return user.StatisticsVisibilitySettings;
    }
}