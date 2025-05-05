using System.Text.Json.Serialization;
using SayPostMainService.Domain.common.post_aggregates_shared.post_content.post_content_items;
using SharedKernel.common.domain;

namespace SayPostMainService.Domain.common.post_aggregates_shared.post_content;


[JsonDerivedType(typeof(HeadingContentItem), typeDiscriminator: nameof(HeadingContentItem))]
[JsonDerivedType(typeof(ListContentItem), typeDiscriminator: nameof(ListContentItem))]
[JsonDerivedType(typeof(ParagraphContentItem), typeDiscriminator: nameof(ParagraphContentItem))]
[JsonDerivedType(typeof(QuoteContentItem), typeDiscriminator: nameof(QuoteContentItem))]
[JsonDerivedType(typeof(SubheadingContentItem), typeDiscriminator: nameof(SubheadingContentItem))]
public abstract class BasePostContentItem : ValueObject
{
    public abstract ContentItemType ItemType { get; }
    public abstract string ToStorageString();
}