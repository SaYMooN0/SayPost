using MediatR;
using SayPostAuthService.Domain.app_user_aggregate;
using SayPostAuthService.Domain.common;
using SayPostAuthService.Domain.common.interfaces.repositories;
using SharedKernel.common.domain.ids;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostAuthService.Application.app_users.queries;

public record class GetUsernameAndEmailForAppUserQuery(AppUserId UserId) :
    IRequest<ErrOr<(string Username, Email Email)>>;

internal class GetUsernameAndEmailForAppUserQueryHandler
    : IRequestHandler<GetUsernameAndEmailForAppUserQuery, ErrOr<(string Username, Email Email)>>
{
    private readonly IAppUsersRepository _appUsersRepository;


    public GetUsernameAndEmailForAppUserQueryHandler(IAppUsersRepository appUsersRepository) {
        _appUsersRepository = appUsersRepository;
    }

    public async Task<ErrOr<(string Username, Email Email)>> Handle(
        GetUsernameAndEmailForAppUserQuery request, CancellationToken cancellationToken
    ) {
        AppUser? user = await _appUsersRepository.GetByIdAsNoTracking(request.UserId);
        if (user is null) {
            return ErrFactory.NotFound(
                "User not found",
                details: $"App user with id {request.UserId} does not exist"
            );
        }


        return (user.Username, user.Email);
    }
}