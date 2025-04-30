using MediatR;
using SayPostNotificationService.Domain.app_user_aggregate;
using SayPostNotificationService.Domain.common;
using SayPostNotificationService.Domain.common.interfaces.repositories;
using SharedKernel.common.domain.ids;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;
using SharedKernel.date_time_provider;

namespace SayPostNotificationService.Application.app_users.commands;

public record class SetNotificationsAsViewedCommand(AppUserId UserId, HashSet<NotificationId> NotificationIds) :
    IRequest<ErrOrNothing>;

internal class SetNotificationsAsViewedCommandHandler
    : IRequestHandler<SetNotificationsAsViewedCommand, ErrOrNothing>
{
    private readonly IAppUsersRepository _appUsersRepository;

    public SetNotificationsAsViewedCommandHandler(IAppUsersRepository appUsersRepository) {
        _appUsersRepository = appUsersRepository;
    }

    public async Task<ErrOrNothing> Handle(
        SetNotificationsAsViewedCommand request, CancellationToken cancellationToken
    ) {
        AppUser? user = await _appUsersRepository.GetByIdWithNotifications(request.UserId);
        if (user is null) {
            return ErrFactory.NotFound(
                "User not found",
                $"Cannot find user with id: {request.UserId}"
            );
        }

        var res = user.ViewNotifications(request.NotificationIds);
        if (res.IsErr(out var err)) {
            return err;
        }

        await _appUsersRepository.Update(user);
        return ErrOrNothing.Nothing;
    }
}