using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Domain.common.post_aggregates_shared.post_content.post_content_items;

public class HeadingContentItem : BasePostContentItem
{
    private const int MaxLength = 250;
    public string Value { get; }

    private HeadingContentItem(string value) {
        Value = value;
    }
    public override ContentItemType ItemType => ContentItemType.Heading;

    public static ErrOr<HeadingContentItem> Create(string value) {
        if (string.IsNullOrWhiteSpace(value))
            return ErrFactory.InvalidData("Heading cannot be empty");

        if (value.Length > MaxLength)
            return ErrFactory.InvalidData(
                "Heading is too long",
                $"Length is {value.Length} characters. Maximum allowed is {MaxLength}."
            );

        return new HeadingContentItem(value);
    }

    public override string ToStorageString() => Value;
    public static HeadingContentItem FromStorageString(string storageStr) => new(storageStr);
    public override IEnumerable<object> GetEqualityComponents() =>[Value];
}