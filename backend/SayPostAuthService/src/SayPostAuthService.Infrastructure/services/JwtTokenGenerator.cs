using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SayPostAuthService.Domain.app_user_aggregate;
using SayPostAuthService.Domain.common.interfaces;
using SharedKernel.configs;

namespace SayPostAuthService.Infrastructure.services;

internal class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly string _secretKey;
    private readonly string _issuer;
    private readonly string _audience;
    public JwtTokenGenerator(JwtTokenConfig options) {
        _secretKey = options.SecretKey;
        _issuer = options.Issuer;
        _audience = options.Audience;
    }


    public string GenerateToken(AppUser user) {
        Claim[] claims = [new("UserId", user.Id.ToString())];

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _issuer,
            _audience,
            claims,
            expires: DateTime.UtcNow.AddDays(30),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

