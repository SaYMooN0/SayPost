using SharedKernel.common.domain;

namespace SayPostFollowingsService.Domain.app_user_aggregate.events;

public record class UserUnfollowedEvent() : IDomainEvent;