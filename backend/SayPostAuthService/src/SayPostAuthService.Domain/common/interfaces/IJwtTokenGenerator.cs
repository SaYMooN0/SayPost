using SayPostAuthService.Domain.app_user_aggregate;

namespace SayPostAuthService.Domain.common.interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(AppUser user);
}
