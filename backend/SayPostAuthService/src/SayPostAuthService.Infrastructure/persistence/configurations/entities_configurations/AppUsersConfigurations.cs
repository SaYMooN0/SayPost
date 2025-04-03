using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SayPostAuthService.Domain.app_user_aggregate;

namespace SayPostAuthService.Infrastructure.persistence.configurations.entities_configurations;

public class AppUsersConfigurations : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder) {
        builder
            .HasKey(x => x.Id);
        
    }
}