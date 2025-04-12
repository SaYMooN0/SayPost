using InfrastructureShared.persistence.extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SayPostAuthService.Domain.app_user_aggregate;
using SayPostAuthService.Infrastructure.persistence.configurations.value_converters;

namespace SayPostAuthService.Infrastructure.persistence.configurations.entities_configurations;

public class AppUsersConfigurations : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder) {
        builder
            .HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .ValueGeneratedNever()
            .HasGuidBasedIdConversion();

        builder
            .Property(x => x.Email)
            .HasConversion<EmailConverter>();

        builder
            .Property(x => x.PasswordHash);
    }
}