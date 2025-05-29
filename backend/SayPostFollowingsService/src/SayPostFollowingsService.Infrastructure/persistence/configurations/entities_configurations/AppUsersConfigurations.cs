using InfrastructureShared.persistence.extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SayPostFollowingsService.Domain.app_user_aggregate;
using SharedKernel.common.domain.ids;

namespace SayPostFollowingsService.Infrastructure.persistence.configurations.entities_configurations;

internal class AppUsersConfigurations : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder) {
        builder
            .HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .ValueGeneratedNever()
            .HasGuidBasedIdConversion();
        
        builder
            .Property<HashSet<AppUserId>>("_followerIds")
            .HasColumnName("FollowerIds")
            .HasGuidBasedIdsHashSetConversion(); 
        
        builder
            .Property<HashSet<AppUserId>>("_followingIds")
            .HasColumnName("FollowingIds")
            .HasGuidBasedIdsHashSetConversion();
    }
}