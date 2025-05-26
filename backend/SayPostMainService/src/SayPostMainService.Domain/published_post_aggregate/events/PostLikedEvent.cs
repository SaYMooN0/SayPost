using SharedKernel.common.domain;
using SharedKernel.common.domain.ids;

namespace SayPostMainService.Domain.published_post_aggregate.events;

public record class PostLikedEvent(PublishedPostId PostId,AppUserId PostAuthorId, AppUserId UserThatLikedId) : IDomainEvent;