using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SayPostAuthService.Domain.common.interfaces;
using SayPostAuthService.Domain.common.interfaces.repositories;
using SayPostAuthService.Infrastructure.persistence;
using SayPostAuthService.Infrastructure.persistence.repositories;
using SayPostAuthService.Infrastructure.services;
using SharedKernel.date_time_provider;
using SharedKernel.interfaces;

namespace SayPostAuthService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {
        // services
        //     .AddAuthRelatedServices()
        //     .AddMessageBrokerIntegration(configuration)
        //     .AddPersistence(configuration)
        //     .AddEmailService(configuration)
        //     .AddMediatR()
        //     .AddDateTimeService()
        //     ;

        return services;
    }

}