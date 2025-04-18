using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SayPostMainService.Domain.common.post_aggregates_shared;

namespace SayPostMainService.Infrastructure.persistence.configurations.value_converters;

public class PostTitleConverter : ValueConverter<PostTitle, string>
{
    public PostTitleConverter() : base(
        id => id.ToString(),
        value => PostTitle.CreateFromString(value).AsSuccess()
    ) { }
}