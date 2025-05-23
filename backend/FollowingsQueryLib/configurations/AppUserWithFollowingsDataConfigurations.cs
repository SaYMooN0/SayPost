using FollowingsQueryLib.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel.common.domain.ids;

namespace FollowingsQueryLib.configurations;

internal class AppUserWithFollowingsDataConfigurations : IEntityTypeConfiguration<AppUserWithFollowingsData>
{
    public void Configure(EntityTypeBuilder<AppUserWithFollowingsData> builder) {
        builder
            .HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, str => new AppUserId(str));
        
        builder
            .Property<HashSet<AppUserId>>("_followerIds")
            .HasColumnName("FollowerIds")
            .HasConversion(new AppUserIdHashSetConverter(), new AppUserIdHashSetComparer());

        
        builder
            .Property<HashSet<AppUserId>>("_followingIds")
            .HasColumnName("FollowingIds")
            .HasConversion(new AppUserIdHashSetConverter(), new AppUserIdHashSetComparer());
    }
}
