using ApiShared;
using ApiShared.extensions;
using MediatR;
using SayPostAuthService.Api.contracts;
using SayPostAuthService.Application.app_users.commands;
using SayPostAuthService.Domain.common;

namespace SayPostAuthService.Api;

internal static class RootHandlers
{
    // internal static IEndpointRouteBuilder MapRootHandlers(this IEndpointRouteBuilder endpoints) {
    //     endpoints.MapPost("/register", Register)
    //         .WithRequestValidation<RegisterUserRequest>();
    //     endpoints.MapPost("/login", Login)
    //         .WithRequestValidation<LoginUserRequest>();
    //     endpoints.MapPost("/confirm-registration", ConfirmRegistration)
    //         .WithRequestValidation<ConfirmRegistrationRequest>();
    //     endpoints.MapPost("/logout", Logout);
    //
    //     return endpoints;
    // }

   
}