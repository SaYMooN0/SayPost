using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Domain.common.post_aggregates_shared.post_content.post_content_items;

public class SubheadingContentItem : BasePostContentItem
{
    private const int MaxLength = 500;
    public string Value { get; }

    private SubheadingContentItem(string value)
    {
        Value = value;
    }

    public override ContentItemType ItemType => ContentItemType.Subheading;

    public static ErrOr<SubheadingContentItem> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return ErrFactory.InvalidData("Subheading cannot be empty");

        if (value.Length > MaxLength)
            return ErrFactory.InvalidData(
                "Subheading is too long",
                $"Length is {value.Length} characters. Maximum allowed is {MaxLength}."
            );

        return new SubheadingContentItem(value);
    }

    public override string ToStorageString() => Value;
    public static SubheadingContentItem FromStorageString(string storageStr) => new(storageStr);

    public override IEnumerable<object> GetEqualityComponents() => [Value];
}
