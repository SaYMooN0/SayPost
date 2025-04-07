using InfrastructureShared.integration_events.background_service;
using InfrastructureShared.integration_events.integration_events_publisher;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SayPostAuthService.Domain.common.interfaces;
using SayPostAuthService.Domain.common.interfaces.repositories;
using SayPostAuthService.Infrastructure.configs;
using SayPostAuthService.Infrastructure.persistence;
using SayPostAuthService.Infrastructure.persistence.repositories;
using SayPostAuthService.Infrastructure.services;
using SharedKernel.configs;
using SharedKernel.date_time_provider;
using SayPostAuthService.Application.configs;

namespace SayPostAuthService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {
        services
            .AddAuthRelatedServices()
            .AddMessageBrokerIntegration(configuration)
            .AddPersistence(configuration)
            .AddEmailService(configuration)
            .AddMediatR()
            .AddDateTimeService()
            .AddFrontedConfigs(configuration)
            ;

        return services;
    }

    private static IServiceCollection AddAuthRelatedServices(this IServiceCollection services) {
        services.AddSingleton<IPasswordHasher>(new PasswordHasher());
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }

    private static IServiceCollection AddMediatR(this IServiceCollection services) {
        services.AddMediatR(options => options.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection)));

        return services;
    }

    private static IServiceCollection AddMessageBrokerIntegration(this IServiceCollection services,
        IConfiguration configuration) {
        services.Configure<MessageBrokerConfig>(options => configuration.GetSection("MessageBroker").Bind(options));
        services.AddSingleton<IIntegrationEventsPublisher, IntegrationEventsPublisher>();
        services.AddHostedService<ConsumeIntegrationEventsBackgroundService>();

        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration) {
        string dbConnectionString = configuration.GetConnectionString("AuthServiceDb")
                                    ?? throw new Exception("Database connection string is not provided.");
        services.AddDbContext<AuthDbContext>(options => options.UseNpgsql(dbConnectionString));

        services.AddScoped<IAppUsersRepository, AppUsersRepository>();
        services.AddScoped<IUnconfirmedAppUsersRepository, UnconfirmedAppUsersRepository>();

        return services;
    }

    private static IServiceCollection AddEmailService(this IServiceCollection services, IConfiguration configuration) {
        services.Configure<EmailServiceConfig>(options => configuration.GetSection("EmailServiceConfig").Bind(options));
        services.AddTransient<IEmailService, EmailService>();
        return services;
    }

    private static IServiceCollection AddDateTimeService(this IServiceCollection services) {
        services.AddSingleton<IDateTimeProvider, UtcDateTimeProvider>();
        return services;
    }
    internal static IServiceCollection AddFrontedConfigs(
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