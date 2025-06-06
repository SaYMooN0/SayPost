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

        builder
            .Property<HashSet<DraftPostId>>("_draftPostIds")
            .HasColumnName("DraftPostIds")
            .HasGuidBasedIdsHashSetConversion();

        builder
            .Property<HashSet<PublishedPostId>>("_publishedPostIds")
            .HasColumnName("PublishedPostIds")
            .HasGuidBasedIdsHashSetConversion();
        
        builder
            .Property<HashSet<PublishedPostId>>("_likedPostIds")
            .HasColumnName("LikedPostIds")
            .HasGuidBasedIdsHashSetConversion();
        
        builder
            .Property<HashSet<PostCommentId>>("_leftCommentIds")
            .HasColumnName("LeftCommentIds")
            .HasGuidBasedIdsHashSetConversion();

        builder
            .HasOne(x => x.ProfileBanner)
            .WithOne()
            .HasForeignKey<UserProfileBanner>("UserId");

        builder
            .ComplexProperty<StatisticsVisibilitySettings>("StatisticsVisibilitySettings", sa =>
            {
                sa.Property(s => s.PublishedPostsOnlyForFollowers)
                    .HasColumnName("StatsVis_PublishedPostsOnlyForFollowers");

                sa.Property(s => s.FollowersOnlyForFollowers)
                    .HasColumnName("StatsVis_FollowersOnlyForFollowers");

                sa.Property(s => s.FollowingOnlyForFollowers)
                    .HasColumnName("StatsVis_FollowingOnlyForFollowers");

                sa.Property(s => s.LikedPostsOnlyForFollowers)
                    .HasColumnName("StatsVis_LikedPostsOnlyForFollowers");

                sa.Property(s => s.LeftCommentsOnlyForFollowers)
                    .HasColumnName("StatsVis_LeftCommentsOnlyForFollowers");
            });
    }
}