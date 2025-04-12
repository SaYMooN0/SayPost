using MediatR;
using SayPostAuthService.Domain.app_user_aggregate;
using SayPostAuthService.Domain.common;
using SayPostAuthService.Domain.common.interfaces;
using SayPostAuthService.Domain.common.interfaces.repositories;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostAuthService.Application.app_users.commands;

public record class CreateAuthTokenForAppUserCommand(
    Email Email,
    string Password
) : IRequest<ErrOr<string>>;

internal class CreateAuthTokenForAppUserCommandHandler
    : IRequestHandler<CreateAuthTokenForAppUserCommand, ErrOr<string>>
{
    private readonly IAppUsersRepository _appUsersRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public CreateAuthTokenForAppUserCommandHandler(
        IAppUsersRepository appUsersRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator
    ) {
        _appUsersRepository = appUsersRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ErrOr<string>> Handle(
        CreateAuthTokenForAppUserCommand request, CancellationToken cancellationToken
    ) {
        AppUser? user = await _appUsersRepository.GetByEmailAsNoTracking(request.Email);
        if (user is null) {
            return ErrFactory.NotFound("There is no user with this email");
        }

        if (!user.IsPasswordCorrect(_passwordHasher, request.Password)) {
            return ErrFactory.InvalidData("Incorrect password");
        }

        var token = _jwtTokenGenerator.GenerateToken(user);
        return token;
    }
}