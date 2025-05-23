using FollowingsQueryLib.repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FollowingsQueryLib;

public static class DependencyInjection
{
    public static IServiceCollection AddUserFollowingsReadRepository(
        this IServiceCollection services,
        IConfiguration configuration
    ) {
        var dbConnectionString = configuration.GetConnectionString("FollowingServiceDb")
                                ?? throw new Exception("Database connection string is not provided.");
        services.AddDbContext<UserRelationsDbContext>(options => options.UseNpgsql(dbConnectionString));
        services.AddScoped<IUserFollowingsReadRepository, UserFollowingsReadRepository>();

        return services;
    }
}