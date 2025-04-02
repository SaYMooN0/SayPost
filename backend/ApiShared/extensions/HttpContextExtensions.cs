using Microsoft.AspNetCore.Http;
using SharedKernel.common.domain.ids;

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
}