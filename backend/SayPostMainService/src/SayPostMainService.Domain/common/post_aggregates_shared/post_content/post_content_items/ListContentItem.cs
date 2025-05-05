using System.Collections.Immutable;
using System.Text.Json;
using System.Text.Json.Serialization;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Domain.common.post_aggregates_shared.post_content.post_content_items;

public class ListContentItem : BasePostContentItem
{
    private const int MaxItems = 60;
    private const int MaxItemLength = 250;

    public ImmutableArray<string> Items { get; }

    private ListContentItem(string[] items) {
        Items = [..items];
    }

    public override ContentItemType ItemType => ContentItemType.List;

    public static ErrOr<ListContentItem> Create(IEnumerable<string> items) {
        var list = items.ToArray();

        if (list.Length > MaxItems)
            return ErrFactory.InvalidData(
                "Too many list items",
                $"List contains {list.Length} items. Maximum allowed is {MaxItems}."
            );

        for (int i = 0; i < list.Length; i++) {
            var item = list[i];
            if (string.IsNullOrWhiteSpace(item)) {
                return ErrFactory.InvalidData("List item cannot be empty", $"Item #{i + 1} is empty");
            }

            if (item.Length > MaxItemLength) {
                return ErrFactory.InvalidData(
                    "List item is too long",
                    $"Item #{i + 1} is {item.Length} characters. Maximum allowed is {MaxItemLength}."
                );
            }
        }

        return new ListContentItem(list);
    }

    public override string ToStorageString() =>
        JsonSerializer.Serialize(this.Items);

    public static ListContentItem FromStorageString(string str) {
        var items = JsonSerializer.Deserialize<string[]>(str)
                    ?? throw new InvalidDataException("Invalid JSON for ListContentItem");

        return new ListContentItem(items);
    }

    public override IEnumerable<object> GetEqualityComponents() => Items;
}