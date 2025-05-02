using SayPostMainService.Domain.common;
using SharedKernel.common.domain;
using SharedKernel.common.domain.ids;

namespace SayPostMainService.Domain.published_post_aggregate.events;

public record NewPublishedPostCreatedEvent(PublishedPostId PostId, AppUserId AuthorId, PostTagId[] PostTags)
    : IDomainEvent;