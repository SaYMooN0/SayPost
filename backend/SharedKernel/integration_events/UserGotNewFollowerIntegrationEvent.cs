using SharedKernel.common.domain.ids;

namespace SharedKernel.integration_events;

public record class UserGotNewFollowerIntegrationEvent(AppUserId UserId, AppUserId FollowerId) : IIntegrationEvent;