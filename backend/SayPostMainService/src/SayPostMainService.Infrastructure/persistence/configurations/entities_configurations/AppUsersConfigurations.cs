using InfrastructureShared.persistence.extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SayPostMainService.Domain.app_user_aggregate;
using SayPostMainService.Domain.common;
using SharedKernel.common.domain.ids;

namespace SayPostMainService.Infrastructure.persistence.configurations.entities_configurations;

internal class AppUsersConfigurations : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder) {
        builder
            .HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .ValueGeneratedNever()
            .HasGuidBasedIdConversion();
        
        builder.Ignore(x => x.DraftPostIds);
        builder
            .Property<HashSet<DraftPostId>>("_draftPostIds")
            .HasColumnName("DraftPostIds")
            .HasGuidBasedIdsHashSetConversion();
        
        builder.Ignore(x => x.PublishedPostIds);
        builder
            .Property<HashSet<PublishedPostId>>("_publishedPostIds")
            .HasColumnName("PublishedPostIds")
            .HasGuidBasedIdsHashSetConversion();
    }
}