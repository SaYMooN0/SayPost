using SharedKernel.common.domain.ids;

namespace SharedKernel.integration_events;

public record NewPostPublishedIntegrationEvent(PublishedPostId PostId, AppUserId AuthorId) : IIntegrationEvent;