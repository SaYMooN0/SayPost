using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SayPostMainService.Domain.common;
using SayPostMainService.Domain.draft_post_aggregate;

namespace SayPostMainService.Infrastructure.persistence.configurations.value_converters;

public class PostTagIdConverter : ValueConverter<PostTagId, string>
{
    public PostTagIdConverter() : base(
        id => id.ToString(),
        value => PostTagId.Create(value).AsSuccess()
    ) { }
}