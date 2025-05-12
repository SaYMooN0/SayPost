using InfrastructureShared.persistence.extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SayPostMainService.Domain.app_user_aggregate;
using SayPostMainService.Infrastructure.persistence.configurations.value_converters;

namespace SayPostMainService.Infrastructure.persistence.configurations.entities_configurations;

public class UserProfileBannerConfigurations : IEntityTypeConfiguration<UserProfileBanner>
{
    public void Configure(EntityTypeBuilder<UserProfileBanner> builder) {
        builder
            .HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .ValueGeneratedNever()
            .HasGuidBasedIdConversion();

        builder.Property(x => x.Scale);
        builder.Property(x => x.Design);
        builder.Property(x => x.DesignVariant);

        builder
            .Property(x => x.Colors)
            .HasConversion<HexColorsArrayConverter>()
            .HasMaxLength(40);
    }
}