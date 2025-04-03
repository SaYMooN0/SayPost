
namespace SayPostAuthService.Domain.common.interfaces;

public interface IPasswordHasher
{
    string HashPassword(string password);
    bool VerifyPassword(string hashedPassword, string passwordToCheck);
}
