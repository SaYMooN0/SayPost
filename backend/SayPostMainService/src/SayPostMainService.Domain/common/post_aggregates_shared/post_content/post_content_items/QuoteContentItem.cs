using System.Text.Json;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Domain.common.post_aggregates_shared.post_content.post_content_items;

public class QuoteContentItem : BasePostContentItem
{
    private const int MaxQuoteLength = 500;
    private const int MaxAuthorLength = 150;

    public string Text { get; }
    public string? Author { get; }

    private QuoteContentItem(string text, string? author) {
        Text = text;
        Author = author;
    }

    public override ContentItemType ItemType => ContentItemType.Quote;

    public static ErrOr<QuoteContentItem> Create(string text, string? author = null) {
        if (string.IsNullOrWhiteSpace(text)) {
            return ErrFactory.InvalidData("Quote cannot be empty");
        }

        if (text.Length > MaxQuoteLength)
            return ErrFactory.InvalidData(
                "Quote is too long",
                $"Length is {text.Length} characters. Maximum allowed is {MaxQuoteLength}."
            );

        if (author is { Length: > MaxAuthorLength })
            return ErrFactory.InvalidData(
                "Author is too long",
                $"Length is {author.Length} characters. Maximum allowed is {MaxAuthorLength}."
            );

        return new QuoteContentItem(text, author);
    }

    public override string ToStorageString() => JsonSerializer.Serialize(
        new StorageFormat() { Text = Text, Author = Author }
    );

    private class StorageFormat
    {
        public string Text { get; set; } = string.Empty;
        public string? Author { get; set; } = null;
    }

    public static QuoteContentItem FromStorageString(string storageStr) {
        var d = JsonSerializer.Deserialize<StorageFormat>(storageStr);
        return new QuoteContentItem(d.Text, d.Author);
    }

    public override IEnumerable<object> GetEqualityComponents() => [Text, Author ?? ""];
}