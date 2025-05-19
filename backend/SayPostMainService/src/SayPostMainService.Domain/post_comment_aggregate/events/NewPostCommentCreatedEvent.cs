using SayPostMainService.Domain.common.post_aggregates_shared;
using SharedKernel.common.domain;
using SharedKernel.common.domain.ids;

namespace SayPostMainService.Domain.post_comment_aggregate.events;

public record class NewPostCommentCreatedEvent(
    PublishedPostId PostId,
    AppUserId CommentAuthorId
) : IDomainEvent;