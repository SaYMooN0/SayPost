using SayPostAuthService.Application.configs;
using SayPostAuthService.Domain.common;
using SayPostAuthService.Domain.common.interfaces;
using SayPostAuthService.Domain.common.interfaces.repositories;
using SayPostAuthService.Domain.unconfirmed_app_user_aggregate;
using SharedKernel.common.domain;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostAuthService.Application.unconfirmed_app_users.commands;

public record class CreateNewUnconfirmedAppUserCommand(Email Email, string Password)
    : ICommand<ErrListOr<Email>>;

internal class CreateNewUnconfirmedAppUserCommandHandler
    : ICommandHandler<CreateNewUnconfirmedAppUserCommand, ErrListOr<Email>>
{
    private readonly IAppUsersRepository _appUsersRepository;
    private readonly IUnconfirmedAppUsersRepository _unconfirmedAppUsersRepository;
    private readonly IEmailService _emailService;
    private readonly IPasswordHasher _passwordHasher;
    private readonly FrontendConfig _frontendConfig;

    public CreateNewUnconfirmedAppUserCommandHandler(
        IAppUsersRepository appUsersRepository,
        IUnconfirmedAppUsersRepository unconfirmedAppUsersRepository,
        IEmailService emailService,
        IPasswordHasher passwordHasher,
        FrontendConfig frontendConfig
    ) {
        _appUsersRepository = appUsersRepository;
        _unconfirmedAppUsersRepository = unconfirmedAppUsersRepository;
        _emailService = emailService;
        _passwordHasher = passwordHasher;
        _frontendConfig = frontendConfig;
    }

    public async Task<ErrListOr<Email>> Handle(
        CreateNewUnconfirmedAppUserCommand request, CancellationToken cancellationToken
    ) {
        UnconfirmedAppUser? unconfirmedUser = await _unconfirmedAppUsersRepository.GetByEmail(request.Email);

        if (unconfirmedUser is null) {
            bool anyConfirmedWithThisEmail = await _appUsersRepository.AnyUserWithEmail(request.Email);
            if (anyConfirmedWithThisEmail) {
                return ErrFactory.Conflict(message: "User with this email already exists");
            }

            var userToAddCreationRes = UnconfirmedAppUser.CreateNew(
                request.Email, request.Password, _passwordHasher
            );
            if (userToAddCreationRes.AnyErr(out var errs)) {
                return errs;
            }

            unconfirmedUser = userToAddCreationRes.AsSuccess();
            await _unconfirmedAppUsersRepository.AddNew(unconfirmedUser);
        }
        else {
            unconfirmedUser.OverridePassword(request.Password, _passwordHasher);
            await _unconfirmedAppUsersRepository.Update(unconfirmedUser);
        }

        string link = _frontendConfig.GenerateConfirmationLink(unconfirmedUser);
        var sendingErr = await _emailService.SendRegistrationConfirmationLink(unconfirmedUser.Email, link);
        if (sendingErr.IsErr(out var err)) {
            return err;
        }

        return unconfirmedUser.Email;
    }
}