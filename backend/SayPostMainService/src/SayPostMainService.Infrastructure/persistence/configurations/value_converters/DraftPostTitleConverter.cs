using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SayPostMainService.Domain.draft_post_aggregate;

namespace SayPostMainService.Infrastructure.persistence.configurations.value_converters;

public class DraftPostTitleConverter : ValueConverter<DraftPostTitle, string>
{
    public DraftPostTitleConverter() : base(
        id => id.ToString(),
        value => DraftPostTitle.CreateNew()
    ) { }
}