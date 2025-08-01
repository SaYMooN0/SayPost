﻿using ApiShared;
using SayPostAuthService.Domain.common;
using SharedKernel.common.errs.utils;

namespace SayPostAuthService.Api.contracts;

internal class LoginUserRequest : IRequestWithValidationNeeded
{
    public string Email { get; init; }
    public string Password { get; init; }

    public RequestValidationResult Validate() {
        if (string.IsNullOrWhiteSpace(Email)) {
            return ErrFactory.NoValue("Email is required");
        }

        if (!Domain.common.Email.IsStringValidEmail(Email)) {
            return ErrFactory.InvalidData("Incorrect email format");
        }

        if (string.IsNullOrWhiteSpace(Password)) {
            return ErrFactory.InvalidData(message: "Password is required");
        }

        return RequestValidationResult.Success;
    }

    public Email ParsedEmail => Domain.common.Email.Create(Email).AsSuccess();
}