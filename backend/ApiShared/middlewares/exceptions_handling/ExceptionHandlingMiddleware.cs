using Microsoft.AspNetCore.Http;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace ApiShared.middlewares.exceptions_handling;

internal class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next) {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context) {
        try {
            await _next(context);
        }
        catch (ErrCausedException ex) {
            //_logger.LogWarning("Handled ErrCausedException: {Message}", ex);
            var errorResponse = CustomResults.ErrorResponse(ex.Err);
            await errorResponse.ExecuteAsync(context);
            return;
        }
        catch (Exception ex) {
            //_logger.LogError(ex, "Unhandled exception occurred.");
            var serverError = new Err(
                message: "Server error occurred. Please try again later"
            );
            var errorResponse = CustomResults.ErrorResponse(serverError);
            await errorResponse.ExecuteAsync(context);
            return;
        }
    }
}