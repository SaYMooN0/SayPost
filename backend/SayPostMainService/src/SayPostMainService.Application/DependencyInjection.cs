using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SayPostMainService.Application.mediatr_behavior.pipelines;

namespace SayPostMainService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services) {
        services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection));
                cfg.AddOpenBehavior(typeof(AccessCheckToModifyDraftPostBehaviorPipeline<,>));
            }
        );
        return services; 
    }
}