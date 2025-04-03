using SayPostAuthService.Domain.app_user_aggregate.events;
using SayPostAuthService.Domain.common;
using SayPostAuthService.Domain.common.interfaces;
using SayPostAuthService.Domain.rules;
using SharedKernel.common.domain;
using SharedKernel.common.errs;

namespace SayPostAuthService.Domain.unconfirmed_app_user_aggregate;

public class UnconfirmedAppUser : AggregateRoot<UnconfirmedAppUserId>
{
    private UnconfirmedAppUser() { }
    private string PasswordHash { get; }
    public Email Email { get; }
    public string ConfirmationCode { get; }
    private UnconfirmedAppUser(UnconfirmedAppUserId id, string passwordHash, Email email, string confirmationCode) {
        Id = id;
        PasswordHash = passwordHash;
        Email = email;
        ConfirmationCode = confirmationCode;
    }

    public static ErrListOr<UnconfirmedAppUser> CreateNew(
        string email, string password, IPasswordHasher passwordHasher
    ) {
        ErrList errList = new();

        var emailResult = Email.Create(email);
        errList.AddPossibleErr(emailResult);
        errList.AddPossibleErr(PasswordRules.CheckForErr(password));

        if (errList.Any()) {
            return errList;
        }

        string passwordHash = passwordHasher.HashPassword(password);

        return new UnconfirmedAppUser(
            UnconfirmedAppUserId.CreateNew(),
            passwordHash,
            emailResult.AsSuccess(),
            confirmationCode: Guid.NewGuid().ToString()
        );
    }

    public ErrOrNothing Confirm(string confirmationCode) {
        if (confirmationCode != this.ConfirmationCode) {
            return new Err(message: "Unable to confirm user. Incorrect confirmation string was provided");
        }

        _domainEvents.Add(new UserConfirmedEvent(Id, Email, PasswordHash));
        return ErrOrNothing.Nothing;
    }
}