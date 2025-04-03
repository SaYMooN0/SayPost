using SayPostAuthService.Domain.common.interfaces;

namespace SayPostAuthService.Infrastructure.services;

public class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password) =>
        BCrypt.Net.BCrypt.HashPassword(password);

    public bool VerifyPassword(string hashedPassword, string passwordToCheck) =>
        BCrypt.Net.BCrypt.Verify(passwordToCheck, hashedPassword);
}