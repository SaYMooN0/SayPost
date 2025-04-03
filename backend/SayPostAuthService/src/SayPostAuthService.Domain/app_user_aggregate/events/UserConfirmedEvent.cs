using SayPostAuthService.Domain.common;
using SharedKernel.common.domain;

namespace SayPostAuthService.Domain.app_user_aggregate.events;

public record class UserConfirmedEvent(
    UnconfirmedAppUserId UnconfirmedAppUserId,
    Email Email,
    string PasswordHash
) : IDomainEvent;