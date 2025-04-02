using ApiShared;
using SayPostAuthService.Domain.rules;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostAuthService.Api.contracts;

internal class RegisterUserRequest : IRequestWithValidationNeeded
{
    public string Email { get; init; }
    public string Password { get; init; }
    public string ConfirmPassword { get; init; }

    public RequestValidationResult Validate() {
        ErrList errs = new();
        if (string.IsNullOrWhiteSpace(Email)) {
            errs.Add(ErrFactory.NoValue("Email is required"));
        }
        else if (!Domain.common.Email.IsStringValidEmail(Email)) {
            return ErrFactory.IncorrectFormat(message: "Email is not valid");
        }

        if (PasswordRules.CheckForErr(Password).IsErr(out var err)) {
            return err;
        }

        if (Password != ConfirmPassword) {
            return ErrFactory.InvalidData(message: "Passwords do not match");
        }

        return RequestValidationResult.Success;
    }
}