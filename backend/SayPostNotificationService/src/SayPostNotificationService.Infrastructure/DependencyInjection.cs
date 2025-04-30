using InfrastructureShared.integration_events.background_service;
using InfrastructureShared.integration_events.integration_events_publisher;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SayPostNotificationService.Domain.common.interfaces.repositories;
using SayPostNotificationService.Infrastructure.persistence;
using SayPostNotificationService.Infrastructure.persistence.repositories;
using SharedKernel.configs;
using SharedKernel.date_time_provider;

namespace SayPostNotificationService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {
        services
            .AddAuthRelatedServices(configuration)
            .AddMessageBrokerIntegration(configuration)
            .AddPersistence(configuration)
            .AddMediatR()
            .AddDateTimeService()
            ;

        return services;
    }

    private static IServiceCollection AddAuthRelatedServices(
        this IServiceCollection services, IConfiguration configuration
    ) {
        var jwtTokenConfig = configuration.GetSection("JwtTokenConfig").Get<JwtTokenConfig>();
        if (jwtTokenConfig is null) {
            throw new Exception("JWT token config not configured");
        }

        services.AddSingleton(jwtTokenConfig);
        return services;
    }

    private static IServiceCollection AddMediatR(this IServiceCollection services) {
        services.AddMediatR(options =>
            options.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection))
        );

        return services;
    }

    private static IServiceCollection AddMessageBrokerIntegration(
        this IServiceCollection services, IConfiguration configuration
    ) {
        services.Configure<MessageBrokerConfig>(options => configuration.GetSection("MessageBroker").Bind(options));
        services.AddSingleton<IIntegrationEventsPublisher, IntegrationEventsPublisher>();
        services.AddHostedService<ConsumeIntegrationEventsBackgroundService>();

        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration) {
        string dbConnectionString = configuration.GetConnectionString("NotificationsServiceDb")
                                    ?? throw new Exception("Database connection string is not provided.");
        services.AddDbContext<NotificationDbContext>(options => options.UseNpgsql(dbConnectionString));

        services.AddScoped<IAppUsersRepository, AppUsersRepository>();

        return services;
    }

    private static IServiceCollection AddDateTimeService(this IServiceCollection services) {
        services.AddSingleton<IDateTimeProvider, UtcDateTimeProvider>();
        return services;
    }
}