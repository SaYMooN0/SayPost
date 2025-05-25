using SharedKernel.common.domain.ids;

namespace SharedKernel.integration_events;

public record PostLikedIntegrationEvent(PublishedPostId PostId, AppUserId PostAuthorId, AppUserId UserThatLikedId)
    : IIntegrationEvent;