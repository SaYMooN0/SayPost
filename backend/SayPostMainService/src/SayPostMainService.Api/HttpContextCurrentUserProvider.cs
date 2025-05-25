using ApiShared.extensions;
using SayPostMainService.Application.interfaces;
using SharedKernel.common.domain.ids;
using SharedKernel.common.errs;
using SharedKernel.configs;

namespace SayPostMainService.Api;

public class HttpContextCurrentUserProvider : ICurrentActorProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly JwtTokenConfig jwtTokenConfig;

    public HttpContextCurrentUserProvider(
        IHttpContextAccessor httpContextAccessor, JwtTokenConfig jwtTokenConfig
    ) {
        _httpContextAccessor = httpContextAccessor;
        this.jwtTokenConfig = jwtTokenConfig;
    }

    public ErrOr<AppUserId> UserId => _httpContextAccessor.HttpContext.ParseUserIdFromJwtToken(jwtTokenConfig);
}