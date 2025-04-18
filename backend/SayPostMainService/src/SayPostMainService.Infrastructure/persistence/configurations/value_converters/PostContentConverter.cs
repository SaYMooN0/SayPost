using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SayPostMainService.Domain.common.post_aggregates_shared;

namespace SayPostMainService.Infrastructure.persistence.configurations.value_converters;

public class PostContentConverter : ValueConverter<PostContent, string>
{
    public PostContentConverter() : base(
        id => id.ToString(),
        value => PostContent.CreateFromString(value).AsSuccess()
    ) { }
}