using SharedKernel.common.domain.ids;

namespace SharedKernel.integration_events;

public record class NewCommentUnderPostLeftIntegrationEvent(
    AppUserId PostAuthorId,
    string PostTitle,
    AppUserId CommentAuthorId
) : IIntegrationEvent;