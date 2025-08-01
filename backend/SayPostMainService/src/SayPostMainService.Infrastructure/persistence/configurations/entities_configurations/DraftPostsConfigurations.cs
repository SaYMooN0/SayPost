using InfrastructureShared.persistence.extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SayPostMainService.Domain.draft_post_aggregate;
using SayPostMainService.Infrastructure.persistence.configurations.value_converters;

namespace SayPostMainService.Infrastructure.persistence.configurations.entities_configurations;

internal class DraftPostsConfigurations : IEntityTypeConfiguration<DraftPost>
{
    public void Configure(EntityTypeBuilder<DraftPost> builder) {
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
            .Property(x => x.CreatedAt);
    }
}