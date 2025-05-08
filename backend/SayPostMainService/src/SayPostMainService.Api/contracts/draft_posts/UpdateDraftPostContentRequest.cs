using System.Text.Json;
using System.Text.Json.Serialization;
using ApiShared;
using SayPostMainService.Domain.common.post_aggregates_shared.post_content;
using SayPostMainService.Domain.common.post_aggregates_shared.post_content.post_content_items;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Api.contracts.draft_posts;

internal class UpdateDraftPostContentRequest : IRequestWithValidationNeeded
{
    public UpdatePostRequestContentItemDto[] NewPostContent { get; init; } = [];

    public RequestValidationResult Validate() {
        if (NewPostContent.Length > PostContent.MaxItems) {
            return ErrFactory.LimitExceeded(
                $"Too many post content items. Maximum allowed items is {PostContent.MaxItems}",
                $"Current content item count is {NewPostContent.Length}"
            );
        }

        ErrList errs = new();

        for (int i = 0; i < NewPostContent.Length; i++) {
            var item = NewPostContent[i];
            var index = i + 1;

            if (string.IsNullOrWhiteSpace(item.Type)) {
                errs.Add(ErrFactory.InvalidData(
                    $"Item #{index} was saved incorrectly",
                    "Missing $type property"
                ));
                continue;
            }

            var parseResult = TryParseItem(item);
            if (parseResult.IsErr(out var err)) {
                errs.Add(ErrFactory.InvalidData(
                    $"Item #{index}: {err.Message}", err.Details
                ));
            }
        }

        return errs;
    }


    private static ErrOr<BasePostContentItem> TryParseItem(UpdatePostRequestContentItemDto dto) {
        return dto.Type switch {
            nameof(HeadingContentItem) =>
                HeadingContentItem.Create(dto.GetString("value"))
                    .Match<ErrOr<BasePostContentItem>>(s => s, e => e),

            nameof(SubheadingContentItem) =>
                SubheadingContentItem.Create(dto.GetString("value"))
                    .Match<ErrOr<BasePostContentItem>>(s => s, e => e),

            nameof(ParagraphContentItem) =>
                ParagraphContentItem.Create(dto.GetString("value"))
                    .Match<ErrOr<BasePostContentItem>>(s => s, e => e),

            nameof(ListContentItem) =>
                ListContentItem.Create(dto.GetStringList("items"))
                    .Match<ErrOr<BasePostContentItem>>(s => s, e => e),

            nameof(QuoteContentItem) =>
                QuoteContentItem.Create(
                    dto.GetString("text"),
                    dto.Properties.TryGetValue("author", out var a) ? a.GetString() : null
                ).Match<ErrOr<BasePostContentItem>>(s => s, e => e),

            _ => ErrFactory.InvalidData($"Unknown content item type '{dto.Type}'")
        };
    }


    public PostContent GetParsedPostContent() {
        List<BasePostContentItem> parsedItems = new();

        foreach (var dto in NewPostContent) {
            var result = TryParseItem(dto);
            if (result.IsSuccess(out var postContentItem)) {
                parsedItems.Add(postContentItem);
            }
        }

        return PostContent.Create(parsedItems).AsSuccess();
    }
}

internal class UpdatePostRequestContentItemDto
{
    [JsonPropertyName("$type")] public string Type { get; init; } = default!;

    [JsonExtensionData] public Dictionary<string, JsonElement> Properties { get; init; } = new();

    public string GetString(string key) =>
        Properties.TryGetValue(key, out var val) ? val.GetString() ?? "" : "";

    public List<string> GetStringList(string key) {
        if (!Properties.TryGetValue(key, out var val) || val.ValueKind != JsonValueKind.Array) {
            return [];
        }

        return val.EnumerateArray()
            .Select(x => x.GetString() ?? "")
            .ToList();
    }
}