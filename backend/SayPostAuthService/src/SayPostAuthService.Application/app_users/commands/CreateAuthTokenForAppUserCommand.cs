using SayPostAuthService.Domain.app_user_aggregate;
using SayPostAuthService.Domain.common;
using SayPostAuthService.Domain.common.interfaces;
using SayPostAuthService.Domain.common.interfaces.repositories;
using SharedKernel.common.domain;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostAuthService.Application.app_users.commands;

public record class CreateAuthTokenForAppUserCommand(Email Email, string Password) : ICommand<ErrOr<string>>;

public class CreateAuthTokenForAppUserCommandHandler : ICommandHandler<CreateAuthTokenForAppUserCommand, ErrOr<string>>
{
    private readonly IAppUsersRepository _appUsersRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IPasswordHasher _passwordHasher;


    public CreateAuthTokenForAppUserCommandHandler(
        IAppUsersRepository appUsersRepository,
        IJwtTokenGenerator jwtTokenGenerator,
        IPasswordHasher passwordHasher
    ) {
        _appUsersRepository = appUsersRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _passwordHasher = passwordHasher;
    }

    public async Task<ErrOr<string>> Handle(
        CreateAuthTokenForAppUserCommand command, CancellationToken cancellationToken
    ) {
        AppUser? user = await _appUsersRepository.GetByEmailAsNoTracking(command.Email);
        if (user is null) {
            return ErrFactory.NotFound("There is no user with this email");
        }

        if (!user.IsPasswordCorrect(_passwordHasher, command.Password)) {
            return ErrFactory.InvalidData("Incorrect password");
        }

        var token = _jwtTokenGenerator.GenerateToken(user);
        return token;
    }
}