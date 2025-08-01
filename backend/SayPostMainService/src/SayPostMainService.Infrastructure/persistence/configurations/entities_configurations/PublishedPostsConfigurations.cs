﻿using InfrastructureShared.persistence.extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SayPostMainService.Domain.published_post_aggregate;
using SayPostMainService.Infrastructure.persistence.configurations.value_converters;
using SharedKernel.common.domain.ids;

namespace SayPostMainService.Infrastructure.persistence.configurations.entities_configurations;

internal class PublishedPostsConfigurations : IEntityTypeConfiguration<PublishedPost>
{
    public void Configure(EntityTypeBuilder<PublishedPost> builder) {
        builder
            .HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .ValueGeneratedNever()
            .HasGuidBasedIdConversion();

        builder
            .Property(x => x.AuthorId)
            .ValueGeneratedNever()
            .HasGuidBasedIdConversion();

        builder
            .Property(x => x.Title)
            .HasConversion<PostTitleConverter>();

        builder
            .Property(x => x.Content)
            .HasConversion<PostContentConverter>();

        builder
            .Property(x => x.Tags)
            .HasConversion(
                converter: new PostTagIdHashSetConverter(),
                valueComparer: new PostTagIdHashSetComparer()
            );

        builder
            .Property(x => x.PublicationDate);

        builder.Ignore(p => p.LikesCount);
        builder
            .Property<HashSet<AppUserId>>("_likedByUserIds")
            .HasColumnName("LikedByUserIds")
            .HasGuidBasedIdsHashSetConversion();
    }
}