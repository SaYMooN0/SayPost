using FollowingsQueryLib.entities;
using FollowingsQueryLib.repository;
using MediatR;
using SayPostMainService.Domain.common.interfaces.repositories;
using SharedKernel.common.domain.ids;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Application.app_users.queries;

public record class ListUserFollowingIdsQuery(AppUserId UserId)
    : IRequest<ErrOr<IReadOnlyCollection<AppUserId>>>;

internal class ListUserFollowingIdsQueryHandler :
    IRequestHandler<ListUserFollowingIdsQuery, ErrOr<IReadOnlyCollection<AppUserId>>>
{
    private readonly IUserFollowingsReadRepository _userFollowingsReadRepository;
    private readonly IAppUsersRepository _appUsersRepository;

    public ListUserFollowingIdsQueryHandler(
        IUserFollowingsReadRepository userFollowingsReadRepository, IAppUsersRepository appUsersRepository
    ) {
        _userFollowingsReadRepository = userFollowingsReadRepository;
        _appUsersRepository = appUsersRepository;
    }

    public async Task<ErrOr<IReadOnlyCollection<AppUserId>>> Handle(
        ListUserFollowingIdsQuery query, CancellationToken cancellationToken
    ) {
        AppUserWithFollowingsData? userWithFollowings = await _userFollowingsReadRepository.GetUser(query.UserId);
        if (userWithFollowings is null) {
            return ErrFactory.NotFound(
                "User not found", $"Could not get user followers. User {query.UserId} was not found"
            );
        }

        return userWithFollowings.FollowingIds;
    }
}