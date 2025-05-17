using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace ApiShared.middlewares.exceptions_handling;

internal class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger
    ) {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context) {
        try {
            await _next(context);
        }
        catch (PipelineBehaviourException ex) {
            var errorResponse = CustomResults.ErrorResponse(ex.Errs);
            await errorResponse.ExecuteAsync(context);
            return;
        }
        catch (ErrCausedException ex) {
            _logger.LogError(
                "Caught ErrCausedException. error: {error}, inner exception: {innerException}, stack trace: {stackTrace}",
                ex.Err,
                ex.InnerException,
                ex.StackTrace
            );
            var errorResponse = CustomResults.ErrorResponse(ex.Err);
            await errorResponse.ExecuteAsync(context);
            return;
        }
        catch (Exception ex) {
            _logger.LogError(
                "Caught unhandled exception. message: {message}, inner exception: {innerException}, stack trace: {stackTrace}",
                ex.Message,
                ex.InnerException,
                ex.StackTrace
            );
            Err serverError = new(message: "Server error occurred. Please try again later");
            var errorResponse = CustomResults.ErrorResponse(serverError);
            await errorResponse.ExecuteAsync(context);
            return;
        }
    }
}