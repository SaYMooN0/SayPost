﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SayPostFollowingsService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services, IConfiguration configuration
    ) {
        services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection));
            }
        );
        return services; 
    }
}