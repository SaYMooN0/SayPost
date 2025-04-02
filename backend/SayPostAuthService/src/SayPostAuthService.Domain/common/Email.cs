using System.Text.RegularExpressions;
using SharedKernel.common.domain;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostAuthService.Domain.common;

public class Email : ValueObject
{
    private string _email;
    private Email(string email) => _email = email;
    public static ErrOr<Email> Create(string email) {
        if (!IsStringValidEmail(email)) {
            return ErrFactory.InvalidData(message: "Invalid email");
        }
        return new Email(email);
    }
    private const string EmailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

    public static bool IsStringValidEmail(string email) =>
        !string.IsNullOrWhiteSpace(email) && Regex.IsMatch(email, EmailRegex);

    public override IEnumerable<object> GetEqualityComponents() {
        yield return _email;
    }
    public override string ToString() => _email;
}
