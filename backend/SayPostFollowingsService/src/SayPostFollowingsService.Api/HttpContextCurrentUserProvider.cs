using ApiShared.extensions;
using SayPostFollowingsService.Application.interfaces;
using SharedKernel.common.domain.ids;

namespace SayPostFollowingsService.Api;

public class HttpContextCurrentUserProvider : ICurrentActorProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpContextCurrentUserProvider(IHttpContextAccessor httpContextAccessor) {
        _httpContextAccessor = httpContextAccessor;
    }

    public AppUserId AppUserId
    {
        get {
            var httpContext = _httpContextAccessor.HttpContext
                              ?? throw new InvalidOperationException("No HTTP context");

            return httpContext.GetAuthenticatedUserId();
            ;
        }
    }
}