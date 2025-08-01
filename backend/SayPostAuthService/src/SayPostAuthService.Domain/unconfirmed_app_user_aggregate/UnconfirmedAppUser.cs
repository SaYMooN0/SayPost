﻿using SayPostAuthService.Domain.app_user_aggregate.events;
using SayPostAuthService.Domain.common;
using SayPostAuthService.Domain.common.interfaces;
using SayPostAuthService.Domain.rules;
using SharedKernel.common.domain;
using SharedKernel.common.errs;

namespace SayPostAuthService.Domain.unconfirmed_app_user_aggregate;

public class UnconfirmedAppUser : AggregateRoot<UnconfirmedAppUserId>
{
    private UnconfirmedAppUser() { }
    public string PasswordHash { get; private set; }
    public Email Email { get; }
    public string ConfirmationCode { get; }

    private UnconfirmedAppUser(UnconfirmedAppUserId id, string passwordHash, Email email, string confirmationCode) {
        Id = id;
        PasswordHash = passwordHash;
        Email = email;
        ConfirmationCode = confirmationCode;
    }

    public static ErrListOr<UnconfirmedAppUser> CreateNew(
        Email email, string password, IPasswordHasher passwordHasher
    ) {
        if (PasswordRules.CheckForErr(password).IsErr(out var err)) {
            return err;
        }

        string passwordHash = passwordHasher.HashPassword(password);

        return new UnconfirmedAppUser(
            UnconfirmedAppUserId.CreateNew(),
            passwordHash,
            email,
            confirmationCode: Guid.NewGuid().ToString()
        );
    }

    public ErrOrNothing Confirm(string confirmationCode) {
        if (confirmationCode != this.ConfirmationCode) {
            return new Err(message: "Unable to confirm user. Incorrect confirmation data was provided");
        }

        return ErrOrNothing.Nothing;
    }

    public ErrOrNothing OverridePassword(string password, IPasswordHasher passwordHasher) {
        if (PasswordRules.CheckForErr(password).IsErr(out var err)) {
            return err;
        }

        this.PasswordHash = passwordHasher.HashPassword(password);
        return ErrOrNothing.Nothing;
    }
}