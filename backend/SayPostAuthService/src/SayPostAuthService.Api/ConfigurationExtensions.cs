using SayPostAuthService.Application.configs;

namespace SayPostAuthService.Api;

internal static class ConfigurationExtensions
{
    internal static IServiceCollection AddConfigurations(
        this IServiceCollection services, IConfiguration configuration
    ) {
        string frontendUrl = configuration["FrontendUrl"] ??
                             throw new Exception("FrontendUrl is not provided");
        string confirmRegistrationUrl = configuration["ConfirmRegistrationUrl"] ??
                                        throw new Exception("ConfirmRegistrationUrl is not provided");

        FrontendConfig frontendConfig = new(url: frontendUrl, confirmRegistrationUrl: confirmRegistrationUrl);
        services.AddSingleton(frontendConfig);


        return services;
    }
}