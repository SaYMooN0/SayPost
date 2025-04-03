using SharedKernel.common.domain;
using SharedKernel.common.domain.ids;

namespace SayPostAuthService.Domain.app_user_aggregate.events;

public record NewAppUserCreatedEvent(AppUserId CreatedUserId) : IDomainEvent;
