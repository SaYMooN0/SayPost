using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SayPostMainService.Domain.post_tag_aggregate;
using SayPostMainService.Infrastructure.persistence.configurations.value_converters;

namespace SayPostMainService.Infrastructure.persistence.configurations.entities_configurations;

public class PostTagsConfigurations : IEntityTypeConfiguration<PostTag>
{
    public void Configure(EntityTypeBuilder<PostTag> builder) {
        builder
            .HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .ValueGeneratedNever()
            .HasConversion<PostTagIdConverter>();
    }
}