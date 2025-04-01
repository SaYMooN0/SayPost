using Microsoft.AspNetCore.Http;
using SharedKernel.common.errs;

namespace ApiShared;

public class CustomResults
{
    public record class ErrorResponseObject(Err[] errors);

    public static IResult ErrorResponse(Err e) =>
        Results.Json(new ErrorResponseObject([e]), statusCode: e.ToHttpStatusCode());

    public static IResult ErrorResponse(ErrList error) =>
        ErrorResponse(error.ToArray());

    public static IResult ErrorResponse(Err[] errors) {
        if (errors.Length == 1) {
            return Results.Json(new ErrorResponseObject(errors), statusCode: errors[0].ToHttpStatusCode());
        }

        if (errors.Any(err => err.IsClientCaused())) {
            return Results.BadRequest(new ErrorResponseObject(errors));
        }

        return Results.InternalServerError(new ErrorResponseObject(errors));
    }

    public static IResult FromErrOrNothing(ErrOrNothing possibleErr, Func<IResult> successFunc) =>
        possibleErr.IsErr(out var err) ? ErrorResponse(err) : successFunc();

    public static IResult FromErrListOrNothing(ErrListOrNothing possibleErrList, Func<IResult> successFunc) =>
        possibleErrList.IsErr(out var errs) ? ErrorResponse(errs) : successFunc();

    public static IResult FromErrOr<T>(ErrOr<T> errOrValue, Func<T, IResult> successFunc) =>
        errOrValue.Match(successFunc, ErrorResponse);

    public static IResult FromErrListOr<T>(ErrListOr<T> errListOrValue, Func<T, IResult> successFunc) =>
        errListOrValue.Match(successFunc, ErrorResponse);
}