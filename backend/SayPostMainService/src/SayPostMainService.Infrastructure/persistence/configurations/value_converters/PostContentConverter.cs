using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SayPostMainService.Domain.common.post_aggregates_shared.post_content;

namespace SayPostMainService.Infrastructure.persistence.configurations.value_converters;

public class PostContentConverter : ValueConverter<PostContent, string>
{
    public PostContentConverter() : base(
        content => ToStorageString(content),
        value => FromStorageString(value)
    ) { }

    private static string ToStorageString(PostContent content) {
        var storageLines = content.Items
            .Select(item => $"{item.ItemType}|{item.ToStorageString()}")
            .ToArray();

        return JsonSerializer.Serialize(storageLines);
    }

    private static PostContent FromStorageString(string raw) => PostContent.CreateFromStorageString(raw).AsSuccess();
}