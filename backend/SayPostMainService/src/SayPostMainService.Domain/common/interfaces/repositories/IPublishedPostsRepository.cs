using SayPostMainService.Domain.published_post_aggregate;
using SharedKernel.common.domain.ids;

namespace SayPostMainService.Domain.common.interfaces.repositories;

public interface IPublishedPostsRepository
{
    Task Add(PublishedPost post);
    Task<PublishedPost[]> QueryPostsWithFilter(PostsQueryFilter filter);
    Task<bool> AnyPostWithId(PublishedPostId postId);
    Task Update(PublishedPost post);
    Task<PublishedPost?> GetByIdAsNoTracking(PublishedPostId postId);
    Task<PublishedPost?> GetById(PublishedPostId postId);
}