using MediatR;
using SayPostAuthService.Application.configs;
using SayPostAuthService.Domain.common;
using SayPostAuthService.Domain.common.interfaces;
using SayPostAuthService.Domain.common.interfaces.repositories;
using SayPostAuthService.Domain.unconfirmed_app_user_aggregate;
using SharedKernel.common.domain;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostAuthService.Application.unconfirmed_app_users.commands;

public record class CreateNewUnconfirmedAppUserCommand(string email, string password)
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

    public async Task<ErrListOr<Email>> Handle(CreateNewUnconfirmedAppUserCommand request,
        CancellationToken cancellationToken) {
        var userToAddCreationRes = UnconfirmedAppUser.CreateNew(
            request.email, request.password, _passwordHasher
        );
        if (userToAddCreationRes.AnyErr(out var errs)) {
            return errs;
        }

        var userToAdd = userToAddCreationRes.AsSuccess();

        bool anyConfirmedWithThisEmail = await _appUsersRepository.AnyUserWithEmail(userToAdd.Email);
        if (anyConfirmedWithThisEmail) {
            return ErrFactory.Conflict(message: "User with this email already exists");
        }


        UnconfirmedAppUser? existingUnconfirmedUser =
            await _unconfirmedAppUsersRepository.GetByEmail(userToAdd.Email);
        if (existingUnconfirmedUser is null) {
            await _unconfirmedAppUsersRepository.AddNew(userToAdd);
        }
        else {
            await _unconfirmedAppUsersRepository.OverrideExistingWithEmail(userToAdd);
        }

        string link = _frontendConfig.GenerateConfirmationLink(userToAdd);
        var sendingErr = await _emailService.SendRegistrationConfirmationLink(userToAdd.Email, link);
        if (sendingErr.IsErr(out var err)) {
            return err;
        }

        return userToAdd.Email;
    }
}