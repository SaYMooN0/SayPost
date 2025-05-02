using SayPostMainService.Domain.published_post_aggregate;
using SharedKernel.common.domain.ids;

namespace SayPostMainService.Domain.common.interfaces.repositories;

public interface IPublishedPostsRepository
{
    Task Add(PublishedPost post);
    Task<PublishedPost[]> QueryPostsWithComments(PostsQueryFilter filter);
    Task<bool> AnyPostWithId(PublishedPostId postId);
    Task<PublishedPost?> AsNoTrackingWithCommentsById(PublishedPostId id);
    Task<PublishedPost?> GetWithCommentsById(PublishedPostId id);
    Task Update(PublishedPost post);
}