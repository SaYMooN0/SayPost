using SharedKernel.common.domain;
using SharedKernel.common.domain.ids;

namespace SayPostMainService.Domain.published_post_aggregate.events;

public record class PostUnlikedEvent(PublishedPostId PostId, AppUserId UserId) : IDomainEvent;