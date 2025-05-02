using SayPostMainService.Domain.common.post_aggregates_shared;
using SharedKernel.common.domain;
using SharedKernel.common.domain.ids;

namespace SayPostMainService.Domain.published_post_aggregate.events;

public record class NewCommentToPostAddedEvent(
    AppUserId PostAuthorId,
    PostTitle PostTitle,
    AppUserId CommentAuthorId
) : IDomainEvent;