using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SayPostMainService.Domain.draft_post_aggregate;

namespace SayPostMainService.Infrastructure.persistence.configurations.value_converters;

public class DraftPostContentConverter : ValueConverter<DraftPostContent, string>
{
    public DraftPostContentConverter() : base(
        id => id.ToString(),
        value => DraftPostContent.CreateFromString(value).AsSuccess()
    ) { }
}