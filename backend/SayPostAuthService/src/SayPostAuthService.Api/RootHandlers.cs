using ApiShared;
using ApiShared.extensions;
using MediatR;
using SayPostAuthService.Api.contracts;
using SharedKernel.common.errs;

namespace SayPostAuthService.Api;

internal static class RootHandlers
{
    internal static IEndpointRouteBuilder MapRootHandlers(this IEndpointRouteBuilder endpoints) {
        // endpoints.MapPost("/register", Register)
        //     .WithRequestValidation<RegisterUserRequest>();
        // endpoints.MapPost("/login", Login)
        //     .WithRequestValidation<LoginUserRequest>();
        // endpoints.MapPost("/confirm-registration", ConfirmRegistration)
        //     .WithRequestValidation<ConfirmRegistrationRequest>();
        // endpoints.MapPost("/logout", Logout);

        return endpoints;
    }

   
}