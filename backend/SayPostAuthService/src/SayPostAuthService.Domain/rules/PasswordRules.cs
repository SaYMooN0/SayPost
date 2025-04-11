using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostAuthService.Domain.rules;

public static class PasswordRules
{
    public const int MinLength = 8;
    public const int MaxLength = 20;

    public static readonly string IncorrectLengthMessage = "Password must be between 8 and 20 characters";
    public static readonly string MustContainLetterMessage = "Password must contain at least one letter";
    public static readonly string MustContainNumberMessage = "Password must contain at least one number";

    public static ErrOrNothing CheckForErr(string password) {
        int passwordLength = string.IsNullOrEmpty(password) ? 0 : password.Length;
        if (passwordLength < MinLength || passwordLength > MaxLength)
            return ErrFactory.IncorrectFormat(message: IncorrectLengthMessage);

        if (!password.Any(char.IsLetter)) {
            return ErrFactory.IncorrectFormat(message: MustContainLetterMessage);
        }

        if (!password.Any(char.IsDigit)) {
            return ErrFactory.IncorrectFormat(message: MustContainNumberMessage);
        }

        return ErrOrNothing.Nothing;
    }
}