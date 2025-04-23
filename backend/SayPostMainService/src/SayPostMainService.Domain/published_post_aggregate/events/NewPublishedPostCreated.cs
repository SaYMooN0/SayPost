using SharedKernel.common.domain;
using SharedKernel.common.domain.ids;

namespace SayPostMainService.Domain.published_post_aggregate.events;

public record NewPublishedPostCreated(PublishedPostId PostId, AppUserId AuthorId) : IDomainEvent;