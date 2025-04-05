using ApiShared;
using ApiShared.extensions;
using MediatR;
using SayPostAuthService.Api.contracts;
using SayPostAuthService.Application.app_users.commands;
using SayPostAuthService.Application.app_users.queries;
using SayPostAuthService.Application.unconfirmed_app_users.commands;
using SayPostAuthService.Domain.common;
using SharedKernel.configs;

namespace SayPostAuthService.Api.endpoints;

internal static class RootHandlers
{
    internal static IEndpointRouteBuilder MapRootHandlers(this IEndpointRouteBuilder endpoints) {
        endpoints.MapPost("/ping", HandlePingAuth);
        endpoints.MapPost("/register", Register)
            .WithRequestValidation<RegisterUserRequest>();
        endpoints.MapPost("/login", Login)
            .WithRequestValidation<LoginUserRequest>();
        endpoints.MapPost("/confirm-registration", ConfirmRegistration)
            .WithRequestValidation<ConfirmRegistrationRequest>();
        endpoints.MapPost("/logout", Logout);

        return endpoints;
    }

    private static async Task<IResult> HandlePingAuth(
        HttpContext httpContext, ISender mediator, JwtTokenConfig jwtConfig
    ) {
        if (!httpContext.ParseUserIdFromJwtToken(jwtConfig).IsSuccess(out var userId)) {
            return CustomResults.AuthRequired();
        }

        GetUsernameAndEmailForAppUserQuery query = new(userId);
        var result = await mediator.Send(query);

        return CustomResults.FromErrOr(result, (data) => Results.Json(new PingAuthResponse(
            UserId: userId.ToString(),
            Username: data.Username,
            Email: data.Email.ToString()
        )));
    }

    private static async Task<IResult> Register(
        HttpContext httpContext, ISender mediator
    ) {
        RegisterUserRequest request = httpContext.GetValidatedRequest<RegisterUserRequest>();
        CreateNewUnconfirmedAppUserCommand command = new(request.ParsedEmail, request.Password);
        var result = await mediator.Send(command);

        return CustomResults.FromErrListOr(result, (userEmail) =>
            CustomResults.Created(new UnconfirmedAppUserCreatedResponse(userEmail.ToString()))
        );
    }

    private static async Task<IResult> ConfirmRegistration(
        HttpContext httpContext, ISender mediator
    ) {
        ConfirmRegistrationRequest request = httpContext.GetValidatedRequest<ConfirmRegistrationRequest>();

        UnconfirmedAppUserId unconfirmedUserId = new(new(request.UserId));
        ConfirmUserRegistrationCommand command = new(unconfirmedUserId, request.ConfirmationCode);
        var err = await mediator.Send(command);

        return CustomResults.FromErrOrNothing(
            err,
            () => Results.Ok()
        );
    }

    private static async Task<IResult> Login(
        HttpContext httpContext, ISender mediator
    ) {
        var request = httpContext.GetValidatedRequest<LoginUserRequest>();
        CreateAuthTokenForAppUserCommand command = new(request.ParsedEmail, request.Password);
        var result = await mediator.Send(command);

        return CustomResults.FromErrOr(result,
            (token) => {
                httpContext.Response.Cookies.Append(AuthCookieName, token, AuthCookieOptions());
                return Results.Ok();
            }
        );
    }

    private static IResult Logout(
        HttpContext httpContext, ISender mediator
    ) {
        var cookieOptions = new CookieOptions {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddDays(-1)
        };

        httpContext.Response.Cookies.Append(AuthCookieName, "", cookieOptions);
        return Results.Ok();
    }

    private static CookieOptions AuthCookieOptions() => new() {
        HttpOnly = true,
        Secure = true,
        SameSite = SameSiteMode.Strict,
        Expires = DateTime.UtcNow.AddDays(30)
    };

    private const string AuthCookieName = "_token";
}