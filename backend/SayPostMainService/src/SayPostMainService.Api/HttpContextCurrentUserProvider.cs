using ApiShared.extensions;
using SayPostMainService.Application.interfaces;
using SharedKernel.common.domain.ids;
using SharedKernel.common.errs;
using SharedKernel.configs;

namespace SayPostMainService.Api;

public class HttpContextCurrentUserProvider : ICurrentActorProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly JwtTokenConfig _jwtConfig;

    public HttpContextCurrentUserProvider(IHttpContextAccessor httpContextAccessor, JwtTokenConfig jwtConfig) {
        _httpContextAccessor = httpContextAccessor;
        _jwtConfig = jwtConfig;
    }

    public ErrOr<AppUserId> UserId =>
        _httpContextAccessor.HttpContext.ParseUserIdFromJwtToken(_jwtConfig);
}