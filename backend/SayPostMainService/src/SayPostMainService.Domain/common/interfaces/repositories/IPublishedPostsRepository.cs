using SayPostMainService.Domain.published_post_aggregate;

namespace SayPostMainService.Domain.common.interfaces.repositories;

public interface IPublishedPostsRepository
{
    Task Add(PublishedPost post);
}