using System.Collections.Immutable;
using System.Text.Json;
using SayPostMainService.Domain.common.post_aggregates_shared.post_content.post_content_items;
using SharedKernel.common.domain;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Domain.common.post_aggregates_shared.post_content;

public class PostContent : ValueObject
{
    private const int MaxItems = 60;

    public ImmutableArray<BasePostContentItem> Items { get; }

    private PostContent(IEnumerable<BasePostContentItem> items) {
        Items = items.ToImmutableArray();
    }

    public static PostContent Default() => new PostContent([
        HeadingContentItem.Create("Post heading").AsSuccess(),
        ParagraphContentItem.Create("Post text").AsSuccess()
    ]);

    public static ErrOr<PostContent> Create(IEnumerable<BasePostContentItem> items) {
        var itemsArr = items as BasePostContentItem[] ?? items.ToArray();

        if (itemsArr.Length == 0) {
            return ErrFactory.InvalidData("Post content can't be empty");
        }

        if (itemsArr.Length > MaxItems) {
            return ErrFactory.InvalidData(
                $"Post content cannot have more than {MaxItems} items",
                $"Current items count is {itemsArr.Length}"
            );
        }

        return new PostContent(itemsArr);
    }

    public override IEnumerable<object> GetEqualityComponents() => Items;

    public static ErrOr<PostContent> CreateFromStorageString(string raw) {
        string[] lines;
        try {
            lines = JsonSerializer.Deserialize<string[]>(raw) ?? [];
        }
        catch {
            return ErrFactory.InvalidData("Failed to parse PostContent storage string");
        }

        if (lines.Length == 0) {
            return ErrFactory.InvalidData("Post content is empty");
        }

        var items = new List<BasePostContentItem>(lines.Length);

        for (int i = 0; i < lines.Length; i++) {
            var parts = lines[i].Split('|', 2);
            if (parts.Length != 2) {
                return ErrFactory.IncorrectFormat($"Incorrect content item #{i + 1} format");
            }

            var typeStr = parts[0];
            var data = parts[1];

            if (!Enum.TryParse<ContentItemType>(typeStr, out var type)) {
                return ErrFactory.InvalidData($"Unknown post content item #{i + 1} type: {typeStr}");
            }

            try {
                BasePostContentItem item = type switch {
                    ContentItemType.Heading => HeadingContentItem.FromStorageString(data),
                    ContentItemType.Subheading => SubheadingContentItem.FromStorageString(data),
                    ContentItemType.Paragraph => ParagraphContentItem.FromStorageString(data),
                    ContentItemType.List => ListContentItem.FromStorageString(data),
                    ContentItemType.Quote => QuoteContentItem.FromStorageString(data),
                    _ => throw new InvalidDataException($"Unsupported content type: {type}")
                };

                items.Add(item);
            }
            catch {
                return ErrFactory.InvalidData($"Failed to parse content item  #{i + 1} type: {typeStr}");
            }
        }

        return Create(items);
    }
}