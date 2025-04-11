using InfrastructureShared.persistence.extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SayPostAuthService.Domain.unconfirmed_app_user_aggregate;
using SayPostAuthService.Infrastructure.persistence.configurations.value_converters;

namespace SayPostAuthService.Infrastructure.persistence.configurations.entities_configurations;

public class UnconfirmedAppUsersConfigurations : IEntityTypeConfiguration<UnconfirmedAppUser>
{
    public void Configure(EntityTypeBuilder<UnconfirmedAppUser> builder) {
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
            .Property(x => x.ConfirmationCode);
    }
}