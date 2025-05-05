using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Domain.common.post_aggregates_shared.post_content.post_content_items;

public class ParagraphContentItem : BasePostContentItem
{
    private const int MaxLength = 1000;
    public string Value { get; }

    private ParagraphContentItem(string value)
    {
        Value = value;
    }

    public override ContentItemType ItemType => ContentItemType.Paragraph;

    public static ErrOr<ParagraphContentItem> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return ErrFactory.InvalidData("Paragraph cannot be empty");

        if (value.Length > MaxLength)
            return ErrFactory.InvalidData(
                "Paragraph is too long",
                $"Length is {value.Length} characters. Maximum allowed is {MaxLength}."
            );

        return new ParagraphContentItem(value);
    }

    public override string ToStorageString() => Value;
    public static ParagraphContentItem FromStorageString(string storageStr) => new(storageStr);

    public override IEnumerable<object> GetEqualityComponents() => [Value];
}
