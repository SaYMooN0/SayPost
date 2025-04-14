using SharedKernel.common.domain.ids;

namespace SharedKernel.integration_events;

public record class NewAppUserCreatedIntegrationEvent(AppUserId CreatedUserId) : IIntegrationEvent;