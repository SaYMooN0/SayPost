using MediatR;
using SayPostMainService.Domain.app_user_aggregate;
using SayPostMainService.Domain.common.interfaces.repositories;
using SharedKernel.common.domain.ids;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Application.app_users.queries;

public record class GetUserWithProfileBannerQuery(AppUserId UserId) : IRequest<ErrOr<AppUser>>;

internal class GetUserWithProfileBannerQueryHandler : IRequestHandler<GetUserWithProfileBannerQuery, ErrOr<AppUser>>
{
    private readonly IAppUsersRepository _appUsersRepository;

    public GetUserWithProfileBannerQueryHandler(IAppUsersRepository appUsersRepository) {
        _appUsersRepository = appUsersRepository;
    }

    public async Task<ErrOr<AppUser>> Handle(GetUserWithProfileBannerQuery query, CancellationToken cancellationToken) {
        var user = await _appUsersRepository.GetWithBannerAsNoTracking(query.UserId);
        if (user is null) {
            return ErrFactory.NotFound($"User with id {query.UserId} was not found");
        }

        return user;
    }
}