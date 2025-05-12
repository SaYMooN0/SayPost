using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SayPostMainService.Domain.app_user_aggregate.profile_banner;

namespace SayPostMainService.Infrastructure.persistence.configurations.value_converters;

public class HexColorsArrayConverter : ValueConverter<HexColor[], string>
{
    public HexColorsArrayConverter() : base(
        colors => string.Join(',', colors.Select(c => c.ToString())),
        str => str
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(s => HexColor.FromString(s).AsSuccess())
            .ToArray()
    ) { }
}