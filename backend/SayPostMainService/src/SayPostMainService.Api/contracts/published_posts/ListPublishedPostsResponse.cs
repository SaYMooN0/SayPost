using System.Collections.Immutable;
using SayPostMainService.Domain.published_post_aggregate;

namespace SayPostMainService.Api.contracts.published_posts;

public record class ListPublishedPostsResponse(
    PublishedPostDataResponse[] Posts
)
{
    public static ListPublishedPostsResponse FromPosts(ImmutableArray<PublishedPost> posts) => new(
        posts
            .Select(PublishedPostDataResponse.FromPost)
            .ToArray()
    );
}