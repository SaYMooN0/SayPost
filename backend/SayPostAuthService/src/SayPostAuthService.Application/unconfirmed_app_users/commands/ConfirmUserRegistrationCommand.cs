using MediatR;
using SayPostAuthService.Domain.app_user_aggregate;
using SayPostAuthService.Domain.common;
using SayPostAuthService.Domain.common.interfaces.repositories;
using SayPostAuthService.Domain.unconfirmed_app_user_aggregate;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostAuthService.Application.unconfirmed_app_users.commands;

public record class ConfirmUserRegistrationCommand(
    UnconfirmedAppUserId UnconfirmedUserId,
    string ConfirmationString
) : IRequest<ErrOrNothing>;

internal class ConfirmUserRegistrationCommandHandler : IRequestHandler<ConfirmUserRegistrationCommand, ErrOrNothing>
{
    private readonly IUnconfirmedAppUsersRepository _unconfirmedAppUsersRepository;
    private readonly IAppUsersRepository _appUsersRepository;

    public ConfirmUserRegistrationCommandHandler(
        IUnconfirmedAppUsersRepository unconfirmedAppUsersRepository,
        IAppUsersRepository appUsersRepository
    ) {
        _unconfirmedAppUsersRepository = unconfirmedAppUsersRepository;
        _appUsersRepository = appUsersRepository;
    }

    public async Task<ErrOrNothing> Handle(
        ConfirmUserRegistrationCommand request, CancellationToken cancellationToken
    ) {
        UnconfirmedAppUser? unconfirmedAppUser =
            await _unconfirmedAppUsersRepository.GetById(request.UnconfirmedUserId);
        if (unconfirmedAppUser is null) {
            return ErrFactory.NotFound(
                message: "No such unconfirmed user exists",
                details: "Ensure that you used correct link or have not already confirmed this user"
            );
        }

        var confirmationRes = unconfirmedAppUser.Confirm(request.ConfirmationString);
        if (confirmationRes.IsErr(out var err)) {
            return err;
        }

        var appUser = AppUser.CreateNew(unconfirmedAppUser.Email, unconfirmedAppUser.PasswordHash);
        await _unconfirmedAppUsersRepository.Remove(unconfirmedAppUser);
        await _appUsersRepository.Add(appUser);

        return ErrOrNothing.Nothing;
    }
}