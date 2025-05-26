using SharedKernel.common.domain;
using SharedKernel.common.domain.ids;

namespace SayPostFollowingsService.Domain.app_user_aggregate.events;

public record UserGotNewFollowingEvent(AppUserId UserThatFollowedId, AppUserId FollowingId) : IDomainEvent;