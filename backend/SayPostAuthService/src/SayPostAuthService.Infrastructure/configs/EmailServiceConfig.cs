﻿namespace SayPostAuthService.Infrastructure.configs;

internal class EmailServiceConfig
{
    public string Host { get; init; }
    public int Port { get; init; }
    public string Username { get; init; }
    public string Password { get; init; }
}