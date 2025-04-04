using System.Text;
using Microsoft.AspNetCore.Http;
using SharedKernel.common.domain.ids;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;
using SharedKernel.configs;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace ApiShared.extensions;

public static class HttpContextExtensions
{
    public static T GetValidatedRequest<T>(this HttpContext context) where T : class, IRequestWithValidationNeeded {
        if (!context.Items.TryGetValue("validatedRequest", out var validatedRequest)) {
            throw new InvalidDataException(
                "Trying to access validated request on the request that has not passed the validation"
            );
        }

        if (validatedRequest is T request) {
            return request;
        }

        throw new InvalidCastException("Request type mismatch");
    }

    public static AppUserId GetAuthenticatedUserId(this HttpContext context) {
        if (!context.Items.TryGetValue("appUserId", out var userIdObj) || userIdObj is not AppUserId userId) {
            throw new UnauthorizedAccessException("User is not authenticated or AppUserId is missing");
        }

        return userId;
    }

    public static ErrOr<AppUserId> ParseUserIdFromJwtToken(this HttpContext httpContext, JwtTokenConfig jwtConfig) {
        string? token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (string.IsNullOrEmpty(token)) {
            return ErrFactory.AuthRequired(
                "User is not authenticated",
                details: "Log in to your account"
            );
        }

        JwtSecurityTokenHandler handler = new();
        TokenValidationParameters tokenValidationParameters = new() {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = jwtConfig.Issuer,
            ValidAudience = jwtConfig.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.SecretKey))
        };

        try {
            var principal = handler.ValidateToken(token, tokenValidationParameters, out _);
            var userIdClaim = principal.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            if (string.IsNullOrEmpty(userIdClaim)) {
                return ErrFactory.AuthRequired(
                    "User is not authenticated",
                    details: "Log in to your account"
                );
            }

            return new AppUserId(Guid.Parse(userIdClaim));
        }
        catch (Exception) {
            return ErrFactory.AuthRequired(
                "User is not authenticated",
                details: "Log in to your account"
            );
        }
    }
}