using SayPostAuthService.Domain.app_user_aggregate;
using SayPostAuthService.Domain.common;
using SayPostAuthService.Domain.common.interfaces;
using SayPostAuthService.Domain.common.interfaces.repositories;
using SharedKernel.common.domain;
using SharedKernel.common.domain.ids;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostAuthService.Application.app_users.queries;

public record class GetUsernameAndEmailForAppUserQuery(AppUserId UserId) :
    IQuery<ErrOr<(string Username, Email Email)>>;

internal class GetUsernameAndEmailForAppUserQueryHandler
    : IQueryHandler<GetUsernameAndEmailForAppUserQuery, ErrOr<(string Username, Email Email)>>
{
    private readonly IAppUsersRepository _appUsersRepository;


    public GetUsernameAndEmailForAppUserQueryHandler(IAppUsersRepository appUsersRepository) {
        _appUsersRepository = appUsersRepository;
    }

    public async Task<ErrOr<(string Username, Email Email)>> Handle(
        GetUsernameAndEmailForAppUserQuery command, CancellationToken cancellationToken
    ) {
        AppUser? user = await _appUsersRepository.GetByIdAsNoTracking(command.UserId);
        if (user is null) {
            return ErrFactory.NotFound(
                "User not found",
                details: $"App user with id {command.UserId} does not exist"
            );
        }


        return (user.Username, user.Email);
    }
}