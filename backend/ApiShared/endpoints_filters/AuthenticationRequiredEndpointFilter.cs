using ApiShared.extensions;
using Microsoft.AspNetCore.Http;
using SharedKernel.common.errs.utils;
using SharedKernel.configs;

namespace ApiShared.endpoints_filters;

internal class AuthenticationRequiredEndpointFilter : IEndpointFilter
{
    private readonly JwtTokenConfig _jwtConfig;

    public AuthenticationRequiredEndpointFilter(JwtTokenConfig config) {
        _jwtConfig = config;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next) {
        var httpContext = context.HttpContext;
        var userIdOrErr = httpContext.ParseUserIdFromJwtToken(_jwtConfig);

        if (userIdOrErr.IsErr(out var err)) {
            return CustomResults.ErrorResponse(ErrFactory.AuthRequired("Access denied. Authentication required"));
        }

        httpContext.Items["appUserId"] = userIdOrErr.AsSuccess();

        return await next(context);
    }
}