using System.Collections.Immutable;
using SayPostMainService.Domain.published_post_aggregate;

namespace SayPostMainService.Api.contracts.published_posts;

public record class ListPublishedPostPreviewsResponse(
    PublishedPostPreviewResponse[] Posts
)
{
    public static ListPublishedPostPreviewsResponse FromPosts(ImmutableArray<PublishedPost> posts) => new(
        posts
            .Select(PublishedPostPreviewResponse.FromPost)
            .ToArray()
    );
}