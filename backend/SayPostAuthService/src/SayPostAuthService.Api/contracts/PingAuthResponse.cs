namespace SayPostAuthService.Api.contracts;

public record class PingAuthResponse(string UserId, string Username, string Email);