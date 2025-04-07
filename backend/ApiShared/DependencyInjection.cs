using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.configs;

namespace ApiShared;

public static class DependencyInjection
{
    public static IServiceCollection AddAuthTokenConfig(
        this IServiceCollection services, IConfiguration configuration
    ) {
        var jwtTokenConfig = configuration.GetSection("JwtTokenConfig").Get<JwtTokenConfig>();
        if (jwtTokenConfig is null) {
            throw new Exception("JWT token config not configured");
        }
        services.AddSingleton(jwtTokenConfig);
        return services;
    }
}