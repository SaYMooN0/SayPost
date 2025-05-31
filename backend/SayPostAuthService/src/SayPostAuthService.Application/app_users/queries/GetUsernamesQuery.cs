using MediatR;
using SayPostAuthService.Domain.common.interfaces.repositories;
using SharedKernel.common.domain.ids;

namespace SayPostAuthService.Application.app_users.queries;

public record class GetUsernamesQuery(AppUserId[] UserIds) :
    IRequest<Dictionary<AppUserId, string>>;

internal class GetUsernamesQueryHandler
    : IRequestHandler<GetUsernamesQuery, Dictionary<AppUserId, string>>
{
    private readonly IAppUsersRepository _appUsersRepository;


    public GetUsernamesQueryHandler(IAppUsersRepository appUsersRepository) {
        _appUsersRepository = appUsersRepository;
    }

    public async Task<Dictionary<AppUserId, string>> Handle(
        GetUsernamesQuery query, CancellationToken cancellationToken
    ) {
        return await _appUsersRepository.GetUsernamesForUsers(query.UserIds);
    }
}