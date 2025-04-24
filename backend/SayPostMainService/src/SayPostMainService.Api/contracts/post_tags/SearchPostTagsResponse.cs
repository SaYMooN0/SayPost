using SayPostMainService.Domain.common;

namespace SayPostMainService.Api.contracts.post_tags;

public record SearchPostTagsResponse(
    string[] Tags
)
{
    public static SearchPostTagsResponse FromTagsIEnumerable(IEnumerable<PostTagId> tags) => new(
        Tags: tags.Select(t => t.ToString()).ToArray()
    );
}